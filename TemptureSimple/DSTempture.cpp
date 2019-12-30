#include "DSTempture.h"

DSTempture::DSTempture(int pin)
{
	this->pin = pin;
}

float DSTempture::GetTempture(OneWire ds)
{
	byte data[2];

	ds.reset();
	ds.write(0xCC);
	ds.write(0x44);

	delay(1000);

	ds.reset();
	ds.write(0xCC);
	ds.write(0xBE);

	data[0] = ds.read();
	data[1] = ds.read();

	return ((data[1] << 8) | data[0]) * 0.0625;
}

void DSTempture::PrintToSerialPort(OneWire ds)
{
	float temperature = GetTempture(ds);

	Serial.println(temperature);
}