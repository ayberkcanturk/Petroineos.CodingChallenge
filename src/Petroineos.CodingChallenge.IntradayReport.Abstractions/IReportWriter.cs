namespace Petroineos.CodingChallenge.IntradayReport.Abstractions
{
    public interface IReportWriter<in TEntity>
    {
        public Task WriteAsync(string? folderPath, 
            string fileName,
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken);
    }
}