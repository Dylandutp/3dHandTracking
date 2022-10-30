import serial

PORT = 'COM3'
BAUD_RATES = 115200
ser = serial.Serial(PORT, BAUD_RATES)
ser.flushInput()
DATA = []
x = 0
while x < 15:
    data_raw = ser.readline()
    data = data_raw.decode()
    DATA.append(data)
    print(DATA)
    DATA.clear()
    x += 1