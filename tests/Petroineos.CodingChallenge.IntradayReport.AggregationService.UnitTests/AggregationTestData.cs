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
                "Period,Volume\r\n1,150\r\n2,150\r\n3,150\r\n4,150\r\n5,150\r\n6,150\r\n7,150\r\n8,150\r\n9,150\r\n10,150\r\n11,150\r\n12,150\r\n13,80\r\n14,80\r\n15,80\r\n16,80\r\n17,80\r\n18,80\r\n19,80\r\n20,80\r\n21,80\r\n22,80\r\n23,80\r\n24,80\r\n"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
