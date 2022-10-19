namespace Petroineos.CodingChallenge.IntradayReport.AggregationService.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan ToTimeInPreviousDay(this TimeSpan timeSpan)
        {
            if (timeSpan >= MidNight) return timeSpan;

            timeSpan = timeSpan.Add(new TimeSpan((timeSpan.Days + 1) * 24, 0, 0));

            return timeSpan;
        }

        public static TimeSpan MidNight => new(0, 0, 0);
    }
}
