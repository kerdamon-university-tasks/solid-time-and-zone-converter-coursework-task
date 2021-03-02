using System.Collections.Generic;

namespace SolidTimeAndZoneConverter
{
    internal static class Program
    {
        private static void Main(string[] arguments)
        {
            var timeZoneNameMap = new Dictionary<string, TimeZone>
            {
                {"GMT", new TimeZone {DifferenceFromUtc = new TimeInterval("0:0:0")}},
                {"PDT", new TimeZone {DifferenceFromUtc = new TimeInterval("-7:0:0")}}
            };
            var app = new App(new ShortArgumentParser(arguments, timeZoneNameMap), new ConsoleConversionResultPrinter());
            app.Launch();
        }
    }

    internal class App
    {
        private readonly ArgumentParser _argumentParser;
        private readonly ConversionResultPrinter _conversionResultPrinter;
        
        public App(ArgumentParser argumentParser, ConversionResultPrinter conversionResultPrinter)
        {
            _argumentParser = argumentParser;
            _conversionResultPrinter = conversionResultPrinter;
        }
        
        public void Launch()
        {
            var timeConversionData = _argumentParser.Parse();
            var convertedTime = TimezoneConverter.Convert(timeConversionData);
            _conversionResultPrinter.Print(convertedTime); 
        }
    }
}