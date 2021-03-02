using System;

namespace SolidTimeAndZoneConverter
{
    public class ConsoleConversionResultPrinter : ConversionResultPrinter
    {
        public void Print(Time convertedTime)
        {
            Console.WriteLine(convertedTime);
        }
    }
}