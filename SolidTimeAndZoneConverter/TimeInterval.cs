using System;

namespace SolidTimeAndZoneConverter
{
    public readonly struct TimeInterval
    {
        public TimeSpan InnerInterval { get; }

        public TimeInterval(string timeIntervalString)
        {
            InnerInterval = TimeSpan.Parse(timeIntervalString);
        }

        private TimeInterval(TimeSpan innerInterval)
        {
            InnerInterval = innerInterval;
        }

        public static TimeInterval operator -(TimeInterval t1, TimeInterval t2)
        {
            return new TimeInterval(t1.InnerInterval - t2.InnerInterval);
        }
    }
}