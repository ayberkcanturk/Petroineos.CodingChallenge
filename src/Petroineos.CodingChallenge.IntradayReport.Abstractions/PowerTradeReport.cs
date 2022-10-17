namespace Petroineos.CodingChallenge.IntradayReport.Abstractions
{
    public class PowerTradeReport
    {
        /// <summary>
        /// For csv reader.
        /// </summary>
        public PowerTradeReport()
        {
        }

        public PowerTradeReport(string time, double volume)
        {
            Time = time;
            Volume = volume;
        }

        public string Time { get; set; }
        public double Volume { get; set; }
    }
}
