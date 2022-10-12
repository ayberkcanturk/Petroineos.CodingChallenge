namespace Petroineos.CodingChallenge.IntradayReport.Abstractions
{
    public interface IIntradayReportAggregationService
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
