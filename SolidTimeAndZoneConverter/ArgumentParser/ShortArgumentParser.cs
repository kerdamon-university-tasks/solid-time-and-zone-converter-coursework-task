using System;
using System.Collections.Generic;

namespace SolidTimeAndZoneConverter
{
    public class ShortArgumentParser : ArgumentParser
    {
        private readonly string[] _arguments;
        private readonly Dictionary<string, TimeZone> _timezoneNameMap;

        public ShortArgumentParser(string[] arguments, Dictionary<string, TimeZone> timezoneNameMap)
        {
            _arguments = arguments;
            _timezoneNameMap = timezoneNameMap;
        }
        
        public TimeConversionData Parse()
        {
            var numberOfArguments = _arguments.Length;
            var timeString = _arguments[0];
            var zoneToBeConvertedName = _arguments[1];
            var zoneAfterConversion = _arguments[2];
            
            if (!ShortArgumentParserInputValidator.IsNumberOfArgumentsValid(numberOfArguments))
                throw new ArgumentException($"Wrong number of arguments (got {numberOfArguments}, should be 3)");

            if (!ShortArgumentParserInputValidator.IsTimeFormatValid(timeString))
                throw new ArgumentException("Time is in wrong format (should be HH:mm:ss)"); 
            
            if (!ShortArgumentParserInputValidator.IsTimeZoneNameValid(zoneToBeConvertedName, _timezoneNameMap))
                throw new ArgumentException($"Invalid time zone name: {zoneToBeConvertedName}"); 
            
            if (!ShortArgumentParserInputValidator.IsTimeZoneNameValid(zoneAfterConversion, _timezoneNameMap))
                throw new ArgumentException($"Invalid time zone name: {zoneAfterConversion}"); 
            
            return new TimeConversionData
            {
                ZoneAfterConversion = _timezoneNameMap[zoneAfterConversion], 
                ZoneToBeConverted = _timezoneNameMap[zoneToBeConvertedName],
                Time = new Time(timeString)
            };
        }

        private static class ShortArgumentParserInputValidator
        {
            public static bool IsNumberOfArgumentsValid(int numberOfArguments)
            {
                return numberOfArguments == 3;
            }

            public static bool IsTimeFormatValid(string timeString)
            {
                return Time.IsFormatValid(timeString);
            }

            public static bool IsTimeZoneNameValid(string timeZoneName, Dictionary<string, TimeZone> timeZoneNameMap)
            {
                try
                {
                    _ = timeZoneNameMap[timeZoneName];
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}