namespace Petroineos.CodingChallenge.IntradayReport.AggregationService.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan ToTime(this TimeSpan timeSpan)
        {
            var time = TimeSpan.FromTicks(timeSpan.Ticks % new TimeSpan(24, 0, 0).Ticks);

            return timeSpan.IsNegative() ? time.Add(new TimeSpan(24, 0, 0)) : time;
        }

        public static bool IsNegative(this TimeSpan timeSpan) => timeSpan < TimeSpan.Zero;
    }
}
