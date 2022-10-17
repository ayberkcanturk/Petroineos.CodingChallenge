using Petroineos.CodingChallenge.PowerTradeService.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Petroineos.CodingChallenge.IntradayReport.AggregationService.UnitTests
{
    public class AggregationTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new List<PowerTrade>()
                {
                    new PowerTrade(){ Date = new DateTime(2022,10,12, 23,00,00), Periods = new PowerPeriod[]
                    {
                        new PowerPeriod() { Period = 1, Volume = 100 },
                        new PowerPeriod() { Period = 2, Volume = 100 },
                        new PowerPeriod() { Period = 3, Volume = 100 },
                        new PowerPeriod() { Period = 4, Volume = 100 },
                        new PowerPeriod() { Period = 5, Volume = 100 },
                        new PowerPeriod() { Period = 6, Volume = 100 },
                        new PowerPeriod() { Period = 7, Volume = 100 },
                        new PowerPeriod() { Period = 8, Volume = 100 },
                        new PowerPeriod() { Period = 9, Volume = 100 },
                        new PowerPeriod() { Period = 10, Volume = 100 },
                        new PowerPeriod() { Period = 11, Volume = 100 },
                        new PowerPeriod() { Period = 12, Volume = 100 },
                        new PowerPeriod() { Period = 13, Volume = 100 },
                        new PowerPeriod() { Period = 14, Volume = 100 },
                        new PowerPeriod() { Period = 15, Volume = 100 },
                        new PowerPeriod() { Period = 16, Volume = 100 },
                        new PowerPeriod() { Period = 17, Volume = 100 },
                        new PowerPeriod() { Period = 18, Volume = 100 },
                        new PowerPeriod() { Period = 19, Volume = 100 },
                        new PowerPeriod() { Period = 20, Volume = 100 },
                        new PowerPeriod() { Period = 21, Volume = 100 },
                        new PowerPeriod() { Period = 22, Volume = 100 },
                        new PowerPeriod() { Period = 23, Volume = 100 },
                        new PowerPeriod() { Period = 24, Volume = 100 },
                    }},
                    new PowerTrade(){ Date = new DateTime(2022,10,12, 23,00,00), Periods = new PowerPeriod[]
                    {
                        new PowerPeriod() { Period = 1, Volume = 50 },
                        new PowerPeriod() { Period = 2, Volume = 50 },
                        new PowerPeriod() { Period = 3, Volume = 50 },
                        new PowerPeriod() { Period = 4, Volume = 50 },
                        new PowerPeriod() { Period = 5, Volume = 50 },
                        new PowerPeriod() { Period = 6, Volume = 50 },
                        new PowerPeriod() { Period = 7, Volume = 50 },
                        new PowerPeriod() { Period = 8, Volume = 50 },
                        new PowerPeriod() { Period = 9, Volume = 50 },
                        new PowerPeriod() { Period = 10, Volume = 50 },
                        new PowerPeriod() { Period = 11, Volume = 50 },
                        new PowerPeriod() { Period = 12, Volume = 50 },
                        new PowerPeriod() { Period = 13, Volume = -20 },
                        new PowerPeriod() { Period = 14, Volume = -20 },
                        new PowerPeriod() { Period = 15, Volume = -20 },
                        new PowerPeriod() { Period = 16, Volume = -20 },
                        new PowerPeriod() { Period = 17, Volume = -20 },
                        new PowerPeriod() { Period = 18, Volume = -20 },
                        new PowerPeriod() { Period = 19, Volume = -20 },
                        new PowerPeriod() { Period = 20, Volume = -20 },
                        new PowerPeriod() { Period = 21, Volume = -20 },
                        new PowerPeriod() { Period = 22, Volume = -20 },
                        new PowerPeriod() { Period = 23, Volume = -20 },
                        new PowerPeriod() { Period = 24, Volume = -20 },
                    }}
                },
                "Time,Volume\r\n23:00,150\r\n00:00,150\r\n01:00,150\r\n02:00,150\r\n03:00,150\r\n04:00,150\r\n05:00,150\r\n06:00,150\r\n07:00,150\r\n08:00,150\r\n09:00,150\r\n10:00,150\r\n11:00,80\r\n12:00,80\r\n13:00,80\r\n14:00,80\r\n15:00,80\r\n16:00,80\r\n17:00,80\r\n18:00,80\r\n19:00,80\r\n20:00,80\r\n21:00,80\r\n22:00,80\r\n"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
