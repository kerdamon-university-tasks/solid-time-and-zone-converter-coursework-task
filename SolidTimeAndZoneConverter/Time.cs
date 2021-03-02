#nullable enable
using System;
using System.Globalization;

namespace SolidTimeAndZoneConverter
{
    public readonly struct Time
    {
        private readonly DateTime _innerTime;
        
        public Time(string dateString)
        {
            _innerTime = DateTime.Parse(dateString);
        }

        private Time(DateTime timeTime)
        {
            _innerTime = timeTime;
        }

        public static bool IsFormatValid(string dateString)
        {
            return DateTime.TryParse(dateString, out _);
        }
        
        public static Time operator +(Time d, TimeInterval t)
        {
            return new Time(d._innerTime + t.InnerInterval);
        }
        
        public static bool operator ==(Time lhs, Time rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Time lhs, Time rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var time = (Time) obj;
            return _innerTime.TimeOfDay == time._innerTime.TimeOfDay;
        }

        public override int GetHashCode()
        {
            return _innerTime.GetHashCode();
        }

        public override string ToString()
        {
            return _innerTime.ToString("HH:mm:ss", CultureInfo.CurrentCulture);
        }
    }
}