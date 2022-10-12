namespace Petroineos.CodingChallenge.IntradayReport.Abstractions
{
    public interface IReportWriter
    {
        public Task WriteAsync<TEntity>(string? folderPath, string fileName,
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken);
    }
}