using FluentAssertions;
using Petroineos.CodingChallenge.IntradayReport.AggregationService.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Petroineos.CodingChallenge.IntradayReport.AggregationService.UnitTests
{
    public class TimeSpanExtensionsUnitTests
    {
        [Theory]
        [MemberData(nameof(GetNegativeTestData))]
        public void ShouldNegativeTimeSpanReturnCorrectTimeInPreviousDay(TimeSpan input, TimeSpan expected)
        {
            var time = input.ToTime();

            time.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(GetPositiveTestData))]
        public void ShouldPositiveTimeSpanReturnCorrectTimeInNextDay(TimeSpan input, TimeSpan expected)
        {
            var time = input.ToTime();

            time.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetNegativeTestData()
        {
            yield return new object[]
            {
                new TimeSpan(-23,1,40),
                new TimeSpan(1,1,40)
            };
            yield return new object[]
            {
                new TimeSpan(-47,24,34),
                new TimeSpan(1,24,34)
            };
        }

        public static IEnumerable<object[]> GetPositiveTestData()
        {
            yield return new object[]
            {
                new TimeSpan(23,0,0),
                new TimeSpan(23,0,0)
            };
            yield return new object[]
            {
                new TimeSpan(50,2,10),
                new TimeSpan(2,2,10)
            };
        }

    }
}
