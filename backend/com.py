import serial
from threading import Thread, Event
from time import sleep
from serial.tools import list_ports

def getCOMPorts() -> list:
    return serial.tools.list_ports.comports()

def _receiver(ser, rxCallback, stopEvent):
    while not stopEvent.is_set():
        try:
            data = ser.read(1)   # wait for at least 1 byte
            if data:
                rxCallback(data)

        except serial.SerialException:
            break

class COM:
    def __init__(self, port, baud=9600):
        """
            Open's a COM port at the specified port and baud rate
        """
        self.ser = serial.Serial(
            port=port,
            baudrate=baud,
            timeout=0.1   # seconds
        )
    
    def write(self, bytes: list) -> None:
        """
            Writes a list of bytes to the COM port
        """
        self.ser.write(bytes)
        self.ser.flush()

    def createRXThread(self, rxCallback) -> None:
        """
            Creates a thread dedicated to listening for a response on the COM port.
        """
        self.stopEvent = Event()
        self.rxThread = Thread(
            target=_receiver,
            args=(self.ser, rxCallback, self.stopEvent),
            daemon=True
        )
        self.rxThread.start()

    def close(self) -> None:
        """
            Safely closes COM port and listener thread.
        """
        self.stopEvent.set()
        self.rxThread.join()
        self.ser.close()

def main():
    # Simple byte sending exampel
    ports = getCOMPorts()
    if not ports:
        print("No serial ports found.")
    else:
        for port in sorted(ports):
            print(f"Port: {port.device}")
            print(f"  Description: {port.description}")
            print(f"  Hardware ID: {port.hwid}")
            print("-" * 20)
    port = input("Port: ").upper()

    try:
        com = COM(port)
    except serial.SerialException as e:
        print(f"Failed to open port: {e}")
        return
    print(f"Opened {port} at {9600} baud")

    def callback(data):
        print(f"\nRX: {data.hex()}", end="", flush=True)

    com.createRXThread(callback)

    try:
        while True:
            print("\n\n\n-------------------")
            byteToSend = int(input("Byte: "))
            bytesToSend = bytes([byteToSend])
            com.write(bytesToSend)
            print(f"Sent: {bytesToSend.hex()}")
            sleep(0.5);
    except KeyboardInterrupt:
        print("\nExiting...")
    finally:
        com.close()

if __name__ == "__main__":
    main()
