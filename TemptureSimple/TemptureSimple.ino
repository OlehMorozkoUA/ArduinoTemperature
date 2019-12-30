#include <OneWire.h>
#include "DSTempture.h"

OneWire ds(2);
DSTempture temp(2);

void setup()
{
  Serial.begin(9600);
}

void loop()
{
	temp.PrintToSerialPort(ds);
}

