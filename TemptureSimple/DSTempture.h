#ifndef DSTEMPTURE
#define DSTEMPTURE

#include "Arduino.h"
#include <pins_arduino.h>
#include <OneWire.h>

class DSTempture
{
public:
	DSTempture(int pin);
	void PrintToSerialPort(OneWire ds);

private:
	int pin;
	float GetTempture(OneWire ds);
};

#endif // !DSTEMPTURE
