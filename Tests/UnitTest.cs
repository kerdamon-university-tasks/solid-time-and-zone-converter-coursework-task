using System;
using System.Collections.Generic;
using NUnit.Framework;
using SolidTimeAndZoneConverter;
using TimeZone = SolidTimeAndZoneConverter.TimeZone;

namespace Tests
{
    public class Tests
    {
        private Dictionary<string, TimeZone> _timeZoneNameMap;
        
        [SetUp]
        public void Setup()
        {
            _timeZoneNameMap = new Dictionary<string, TimeZone>
            {
                {"GMT", new TimeZone {DifferenceFromUtc = new TimeInterval("0:0:0")}},
                {"PDT", new TimeZone {DifferenceFromUtc = new TimeInterval("-7:0:0")}},
                {"CKT", new TimeZone {DifferenceFromUtc = new TimeInterval("13:0:0")}},
                {"NPT", new TimeZone {DifferenceFromUtc = new TimeInterval("5:45:0")}}
            };
        }

        [Test]
        public void TestConvertingNegativeDifferenceTimeZones()
        {
            var arguments = new[] {"23:01:03", "GMT", "PDT"};
            var convertedTime = ConvertArgsToTime(arguments);
            Assert.AreEqual(new Time("16:01:03"),  convertedTime);
        }
        
        [Test]
        public void TestConvertingBigDifferenceTimeZones()
        {
            var arguments = new[] {"23:01:03", "GMT", "CKT"};
            var convertedTime = ConvertArgsToTime(arguments);
            Assert.AreEqual(new Time("12:01:03"), convertedTime);
        }
        
        [Test]
        public void TestConvertingSameTimeZones()
        {
            var arguments = new[] {"23:01:03", "GMT", "GMT"};
            var convertedTime = ConvertArgsToTime(arguments);
            Assert.AreEqual(new Time("23:01:03"), convertedTime);
        }
        
        [Test]
        public void TestConvertingTimeZonesWithNonZeroMinutes()
        {
            var arguments = new[] {"23:01:03", "GMT", "NPT"};
            var convertedTime = ConvertArgsToTime(arguments);
            Assert.AreEqual(new Time("04:46:03"), convertedTime);
        }


        [Test]
        public void TestNumberOfArgumentsValidation()
        {
            var arguments = new[] {"23:01:03", "GMT", "PDT", "Something"};
            var ex = Assert.Throws<ArgumentException>(() => ConvertArgsToTime(arguments));
            Assert.That(ex.Message, Is.EqualTo("Wrong number of arguments (got 4, should be 3)"));
        }
        
        [Test]
        public void TestTimeFormatValidation()
        {
            var arguments = new[] {"24:01:03", "GMT", "PDT"};
            var ex = Assert.Throws<ArgumentException>(() => ConvertArgsToTime(arguments));
            Assert.That(ex.Message, Is.EqualTo("Time is in wrong format (should be HH:mm:ss)"));
        }
        
        [Test]
        public void TestZoneToBeConvertedNameValidation()
        {
            var arguments = new[] {"23:01:03", "GM", "PDT"};
            var ex = Assert.Throws<ArgumentException>(() => ConvertArgsToTime(arguments));
            Assert.That(ex.Message, Is.EqualTo("Invalid time zone name: GM"));
        }
        
        [Test]
        public void TestZoneAfterConversionNameValidation()
        {
            var arguments = new[] {"23:01:03", "GMT", "PD"};
            var ex = Assert.Throws<ArgumentException>(() => ConvertArgsToTime(arguments));
            Assert.That(ex.Message, Is.EqualTo("Invalid time zone name: PD"));
        }

        private Time ConvertArgsToTime(string[] arguments)
        {
            var argumentParser = new ShortArgumentParser(arguments, _timeZoneNameMap);
            var timeConversionData = argumentParser.Parse();
            var convertedTime = TimezoneConverter.Convert(timeConversionData);
            return convertedTime;
        }
    }
}