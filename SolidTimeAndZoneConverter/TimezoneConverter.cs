namespace SolidTimeAndZoneConverter
{
    public static class TimezoneConverter
    {
        public static Time Convert(TimeConversionData timeConversionData)
        {
            var timeDifference = CalculateDifference(timeConversionData.ZoneToBeConverted, timeConversionData.ZoneAfterConversion);
            return ApplyTimeDifference(timeConversionData.Time, timeDifference);
        }

        private static TimeInterval CalculateDifference(TimeZone zoneToBeConverted, TimeZone zoneAfterConversion)
        {
            return zoneAfterConversion.DifferenceFromUtc - zoneToBeConverted.DifferenceFromUtc;
        }

        private static Time ApplyTimeDifference(Time time, TimeInterval difference)
        {
            return time + difference;
        }
    }
}