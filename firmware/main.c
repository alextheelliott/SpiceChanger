/*
 * Firmware package for the Automated Spice Rack Project
 * completed in accordance with UBC Mech 423 2025/26
 * 
 * Austin Chuong - 24854184 - 
 * Alex Elliott  - 99567398 - alexjaelliott@gmail.com
 */

#include "driverlib.h"
#include <msp430.h>
#include <stdio.h>
#define BUFFER_SIZE 50
#define START_BYTE 255

// ====================================================================== Structs

typedef struct {
    volatile char buffer[BUFFER_SIZE];
    volatile unsigned int head;
    volatile unsigned int tail;
    volatile unsigned int count;
} Buffer;

// ====================================================================== Global Variables

volatile Buffer rxBuffer = {{}, 0, 0, 0};
volatile Buffer taskBuffer = {{}, 0, 0, 0};

volatile int started = 0;
volatile int state = 0;
volatile unsigned char activeIndex = 0;

volatile bool spiceSeen = false;

volatile long encTicks = 0;
volatile long desTicks = 0;

volatile unsigned int PWM_25;
volatile unsigned int stepsRem = 0;
volatile signed int stepperState = 0;
volatile signed int stepperDir = 0;
//volatile unsigned int stepperSpeed = (8000000 / 5000) - 1;

// ====================================================================== Prototypes

void uartTransmit(int byte);

void setMotorSpeedDirection(int value); // DC Motor

unsigned int stepLookup(int state); // Stepper Motor
void stepMotor(void);

void bufferPush(volatile Buffer *b, int byte);
int bufferPop(volatile Buffer *b);

void init();
void processPacket(void);
void stateMachine(void);

// ====================================================================== Methods

void uartTransmit(int byte) {
    while (!(UCA1IFG & UCTXIFG));         // Wait for TX buffer to be ready
    UCA1TXBUF = byte;                     // Send data
}

void setMotorSpeedDirection(int value) {
    if (value > 100) {
        value = 100;
    } else if (value < 0) {
        value = 0;
    }
    
    // Speed
    TB0CCR2 = TB0CCR0 * abs(value) / 100;

    // Direction
    if (value == 0) {
        P3OUT |= BIT6 + BIT7; 
    } else if (value > 0) {
        P3OUT |= BIT6;
        P3OUT &= ~BIT7;
    } else {
        P3OUT &= ~BIT6;
        P3OUT |= BIT7;
    }
}

unsigned int stepLookup(int state) {
    int table[] = {
        0b1001,
        0b1000,
        0b1100,
        0b0100,
        0b0110,
        0b0010,
        0b0011,
        0b0001,
    };
    return table[state];
}

void stepMotor(void) {
    // lookup the right signals to each stepper wire
    unsigned int value = stepLookup(stepperState);
    uint8_t bit3 = (value >> 3) & 1;
    uint8_t bit2 = (value >> 2) & 1;
    uint8_t bit1 = (value >> 1) & 1;
    uint8_t bit0 = (value >> 0) & 1;
    
    // output either the logic high or logic low
    if (bit3) PJOUT |= BIT0; else PJOUT &= ~BIT0;
    if (bit2) PJOUT |= BIT1; else PJOUT &= ~BIT1;
    if (bit1) PJOUT |= BIT2; else PJOUT &= ~BIT2;
    if (bit0) PJOUT |= BIT3; else PJOUT &= ~BIT3;

    stepperState = (stepperState + stepperDir) & 0x07; // increments state and resets to 0 after 7
    if (stepperState < 0) 
        stepperState = 7;
}

void bufferPush(volatile Buffer *b, int byte) {
    if (b->count < BUFFER_SIZE) {
        b->buffer[b->head] = byte;
        b->head = (b->head + 1) % BUFFER_SIZE;
        b->count++;
    } else {
        return;
    }
}

int bufferPop(volatile Buffer *b) {
    if (b->count > 0) {
        int byte = b->buffer[b->tail];
        b->tail = (b->tail + 1) % BUFFER_SIZE;
        b->count--;
        return byte;
    } else { // Buffer underrun
        return 'U';
    }
}

void init(void) {
    // ------------------------------------------------------------------ Clock Setup

    WDTCTL = WDTPW | WDTHOLD;   // Stop watchdog

    CSCTL0_H = CSKEY_H;                   // Unlock CS registers
    CSCTL1 = DCOFSEL_3;                   // DCORSEL=1 → high freq range, DCOFSEL_3 = ~8 MHz
    CSCTL2 = SELA__DCOCLK | SELS__DCOCLK | SELM__DCOCLK;
    CSCTL3 = DIVA__4 | DIVS__1 | DIVM__1; // No dividers
    CSCTL0_H = 0;                         // Lock CS registers

    // set P3.6-7 to outputs for the offboard motor driver
    P3DIR |= BIT6 | BIT7;
    P3OUT |= BIT6;
    P3OUT &= ~BIT7;

    // ------------------------------------------------------------------ Sensors

    // Configure P1.3 as input
    P1DIR &= ~BIT3;
    P1REN |= BIT3;    // Enable pull resistor (optional but recommended)
    P1OUT &= ~BIT3;   // pull-down (change to |= BIT3 for pull-up)
    // Read initial state
    spiceSeen = !(P1IN & BIT3);

    // Interrupt edge select:
    if (P1IN & BIT3)
        P1IES |= BIT3;     // High → Low
    else
        P1IES &= ~BIT3;    // Low → High

    // Clear any pending interrupt & Enable interrupt
    P1IFG &= ~BIT3;
    P1IE |= BIT3;

    // ------------------------------------------------------------------ Timer A

    // Encoder B setup
    P1DIR &= ~BIT1;
    P1SEL0 &= ~BIT1;
    P1SEL1 |= BIT1;

    TA0CTL = TASSEL__INCLK | MC__CONTINOUS | TACLR;

    // Encoder A setup
    P1DIR &= ~BIT2;
    P1SEL0 &= ~BIT2;
    P1SEL1 |= BIT2;

    TA1CTL = TASSEL__INCLK | MC__CONTINOUS | TACLR;
    
    // ------------------------------------------------------------------ Timer B

    // Motor PWM Timer
    P1DIR |= BIT4 | BIT5;
    P1SEL0 |= BIT4 | BIT5;
    P1SEL1 &= ~(BIT4 | BIT5);

    TB0CTL = TBSSEL__SMCLK | MC__UP | TBCLR; 
    TB0CCR0 = 160 - 1;   // 50 kHz PWM
    TB0CCTL1 = OUTMOD_7;
    TB0CCTL2 = OUTMOD_7;
    TB0CCR1 = 60 * TB0CCR0 / 100; // Const. for Stepper Moter
    TB0CCR2 = 50 * TB0CCR0 / 100; // Variable for DC Motor

    // set PJ.0-3 to outputs for the offboard motor driver
    PJDIR |= BIT0 | BIT1 | BIT2 | BIT3;

    // Encoder Reporting Timer
    TB1CTL = TBSSEL__SMCLK | ID__8 | MC__STOP | TBCLR; 
    TB1CCR0 = 5000 - 1;   // 200 Hz PWM
    TB1CCTL0 = CCIE; 

    // ------------------------------------------------------------------ UART1

    P2SEL0 &= ~(BIT5 | BIT6);
    P2SEL1 |= BIT5 | BIT6;

    UCA1CTLW0 |= UCSWRST;   // Put eUSCI in reset
    UCA1CTLW0 |= UCSSEL__SMCLK; // Select SMCLK as clock source

    // 9600 Baud Rate
    UCA1BRW = 52;                                 // INT(833 / 16) = 52
    UCA1MCTLW = UCOS16 | UCBRF_1 | (0x49 << 8);   // Oversampling, fractional part

    // 38400 Baud Rate
    // UCA1BRW = 13;                                 // INT(833 / 16) = 52
    // UCA1MCTLW = UCOS16 | UCBRF_0 | (0x84 << 8);   // Oversampling, fractional part

    UCA1CTLW0 &= ~UCSWRST;  // Initialize eUSCI
    UCA1IE |= UCRXIE;   // Enable USCI_A0 RX interrupt
}

// Process a packet from the buffer
void processPacket(void) {
    int start = bufferPop(&rxBuffer);
    if (start == START_BYTE) {
        int commandByte = bufferPop(&rxBuffer); // Command
        int index = bufferPop(&rxBuffer);   // Index

        switch(commandByte)
        {
            case 0x00: // Give Spice
                bufferPush(&taskBuffer, (index << 1 && 0x00));
                break;
            case 0x01: // Return Spice
                bufferPush(&taskBuffer, (index << 1 && 0x01));
                break;
            default: break;
        }
    }
}

void stateMachine(void) {
    switch (state) {
        case 0: // Waiting for Task
            if (taskBuffer.count > 0) {
                int task = bufferPop(&taskBuffer);
                state = (task && 0x01 == 0x00) ? 1 : 11;
                activeIndex = task >> 1;
            }
            break;
        // -------------------------------------------------------------- Give Spice
        case 1: // Give Spice - Step 1 - Rotate to correct index
            desTicks = 100 * activeIndex + 200; // TODO replace with real values
            state = 2;
            break;
        case 2: // Give Spice - Step 2 - Wait until index matches (encoder ticks is correct)
            if (abs(desTicks - encTicks) < 5) { state = 3; }
            break;
        case 3: // Give Spice - Step 3 - Extend the pusher arm
            stepperDir = -1;
            stepsRem = 1000; // TODO replace with real values
            state = 4;
            break;
        case 4: // Give Spice - Step 4 - Wait until arm is pushed (stepper steps is completed)
            if (stepsRem < 1) { state = 5; }
            break;
        case 5: // Give Spice - Step 5 - Wait until the user TAKES the container (IR sensor)
            if (!spiceSeen) { state = 6; }
            break;
        case 6: // Give Spice - Step 6 - Retract the pusher arm
            stepperDir = 1;
            stepsRem = 1000;
            state = 7;
            break;
        case 7: // Give Spice - Step 7 - Wait until arm is ractracted (stepper steps is completed)
            if (stepsRem < 1) { state = 8; }
            break;
        case 8: // Give Spice - Step 8 - Continue onto next task
            state = 0;
            break;
        // -------------------------------------------------------------- Return Spice
        case 11: // Return Spice - Step 1 - Rotate to correct index

            break;
        case 12: // Return Spice - Step 2 - Wait until index matches (encoder ticks is correct)

            break;
        case 13: // Return Spice - Step 3 - Extend the pusher arm
        
            break;
        case 14: // Return Spice - Step 4 - Wait until arm is pushed (stepper steps is completed)
        
            break;
        case 15: // Return Spice - Step 5 - Wait until the user RETURNS the container (IR sensor)
        
            break;
        case 16: // Return Spice - Step 6 - Retract the pusher arm
        
            break;
        case 17: // Return Spice - Step 7 - Wait until arm is ractracted (stepper steps is completed)
        
            break;
        case 18: // Return Spice - Step 8 - Continue onto next task
            state = 0;
            break;
        default: state = 0; // Handle broken edge cases
    }
}

// ====================================================================== Main

void main (void) {
    init();
    __bis_SR_register(GIE);  // Enable interrupts globally

    // ------------------------------------------------------------------ Main Loop

    while (1) {
        if (rxBuffer.count >= 5) {  // Ensure enough bytes for a full packet
            processPacket();  // Process incoming packet
        }

        stateMachine();
    }
}

// ====================================================================== ISRs

#pragma vector=PORT1_VECTOR
__interrupt void Port_1_ISR(void) {
    if (P1IFG & BIT3) {
        if (P1IN & BIT3) {
            spiceSeen = false;
            P1IES |= BIT3; // next interrupt on falling edge
        } else {
            spiceSeen = true;
            P1IES &= ~BIT3; // next interrupt on rising edge
        }

        // Clear flag
        P1IFG &= ~BIT3;
    }
}

#pragma vector = TIMER1_B0_VECTOR
__interrupt void TIMER1_B0_ISR(void) {
    encTicks = encTicks + TA0R - TA1R;
    TA0R = 0;
    TA1R = 0;

    if (stepsRem > 0) {
        stepMotor();
        stepsRem--;
    }

    setMotorSpeedDirection(0.5 * (desTicks - encTicks)); // todo replace Kp
}

#pragma vector = USCI_A1_VECTOR
__interrupt void USCI_A1_ISR(void) {
    if (started == 0) {
        started = 1;
        TB1CTL = TBSSEL__SMCLK | ID__8 | MC__CONTINOUS | TBCLR; 
    }

    unsigned char RxByte = 0;
    RxByte = UCA1RXBUF; // Get the new byte from the Rx buffer
    bufferPush(&rxBuffer, RxByte);
}
