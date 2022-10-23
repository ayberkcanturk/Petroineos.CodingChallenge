namespace Petroineos.CodingChallenge.IntradayReport.WindowsService
{
    public class WorkerServiceOptions
    {
        public double TaskDelayInSeconds { get; set; } = 60 * 60;
        public int NumOfRetries { get; set; } = 1;
    }
}
