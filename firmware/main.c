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
volatile long ticks = 0;

// ====================================================================== Prototypes

void uartTransmit(int byte);

void setMotorSpeedDirection(int value);
void setMotorDistance(int dist);

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
    // Speed
    TB0CCR2 = TB0CCR0 * abs(value) / 100;

    // Direction
    if (value > 0) {
        P3OUT |= BIT6;
        P3OUT &= ~BIT7;
    } else {
        P3OUT &= ~BIT6;
        P3OUT |= BIT7;
    }
}

void setMotorDistance(int dist) {

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
    P1DIR |= BIT5;
    P1SEL0 |= BIT5;
    P1SEL1 &= ~BIT5;

    TB0CTL = TBSSEL__SMCLK | MC__UP | TBCLR; 
    TB0CCR0 = 160 - 1;   // 50 kHz PWM
    TB0CCTL2 = OUTMOD_7;
    TB0CCR2 = 50 * TB0CCR0 / 100;

    // Encoder Reporting Timer
    TB1CTL = TBSSEL__SMCLK | ID__8 | MC__STOP | TBCLR; 
    TB1CCR0 = 50000 - 1;   // 20 Hz PWM
    TB1CCTL0 = CCIE; 

    // ------------------------------------------------------------------ UART1

    P2SEL0 &= ~(BIT5 | BIT6);
    P2SEL1 |= BIT5 | BIT6;

    UCA1CTLW0 |= UCSWRST;   // Put eUSCI in reset
    UCA1CTLW0 |= UCSSEL__SMCLK; // Select SMCLK as clock source

    // 9600 Baud Rate
    // UCA1BRW = 52;                                 // INT(833 / 16) = 52
    // UCA1MCTLW = UCOS16 | UCBRF_1 | (0x49 << 8);   // Oversampling, fractional part

    // 38400 Baud Rate
    UCA1BRW = 13;                                 // INT(833 / 16) = 52
    UCA1MCTLW = UCOS16 | UCBRF_0 | (0x84 << 8);   // Oversampling, fractional part

    UCA1CTLW0 &= ~UCSWRST;  // Initialize eUSCI
    UCA1IE |= UCRXIE;   // Enable USCI_A0 RX interrupt
}

// Process a packet from the buffer
void processPacket(void) {
    int start = bufferPop(&rxBuffer);
    if (start == START_BYTE) {
        int commandByte = bufferPop(&rxBuffer);
        int dataByte1 = bufferPop(&rxBuffer); // Direction
        int dataByte2 = bufferPop(&rxBuffer); // Speed
        int dataByte3 = bufferPop(&rxBuffer); // Distance / Quantity

        uartTransmit(dataByte1);
        uartTransmit(dataByte2);
        uartTransmit(dataByte3);

        int direc;

        switch(dataByte1)
        {
            case 0x00: // CW
                direc =  1;
                break;
            case 0x01: // CCW
                direc = -1;
                break;
            default:
                direc =  0;
                break;
        }

        switch(commandByte)
        {
            case 0x01: // Command Word 1 : Step
                break;
            case 0x02: // Command Word 2 : Continuous
                setMotorSpeedDirection(direc * dataByte2);
                break;
            case 0x03: // Command Word 2 : Finite Distance
                setMotorSpeedDirection(direc * dataByte2);
                setMotorDistance(dataByte3);
                break;
            case 0x00: // Command 0: Stop
            default:
                P3OUT |= BIT6 + BIT7;
                break;
        }
    }
}

void stateMachine(void) {
    switch (state) {
        case 0: // Waiting for Task

            break;
        // -------------------------------------------------------------- Give Spice
        case 1: // Give Spice - Step 1

            break;
        case 2: // Give Spice - Step 2

            break;
        case 3: // Give Spice - Step 3
        
            break;
        case 4: // Give Spice - Step 4
        
            break;
        case 5: // Give Spice - Step 5
        
            break;
        case 6: // Give Spice - Step 6
        
            break;
        case 7: // Give Spice - Step 7

            break;
        case 8: // Give Spice - Step 8
        
            break;
        // -------------------------------------------------------------- Return Spice
        case 11: // Return Spice - Step 1
        
            break;
        case 12: // Return Spice - Step 2
        
            break;
        case 13: // Return Spice - Step 3
        
            break;
        case 14: // Return Spice - Step 4
        
            break;
        case 15: // Return Spice - Step 5
        
            break;
        case 16: // Return Spice - Step 6
        
            break;
        case 17: // Return Spice - Step 7
        
            break;
        case 18: // Return Spice - Step 8
        
            break;
    }
}

// ====================================================================== Main

void main (void) {
    init();

    // ------------------------------------------------------------------ Main Loop

    __bis_SR_register(GIE);  // Enable interrupts globally

    while (1) {
        if (rxBuffer.count >= 5) {  // Ensure enough bytes for a full packet
            processPacket();  // Process incoming packet
        }

        stateMachine();
    }
}

// ====================================================================== ISRs

#pragma vector = TIMER1_B0_VECTOR
__interrupt void TIMER1_B0_ISR(void) {
    ticks = ticks + TA0R - TA1R;

    TA0R = 0;
    TA1R = 0;
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

