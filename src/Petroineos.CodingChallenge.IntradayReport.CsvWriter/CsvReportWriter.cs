using Petroineos.CodingChallenge.IntradayReport.Abstractions;
using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Petroineos.CodingChallenge.IntradayReport.AggregationService.UnitTests")]
[assembly: InternalsVisibleTo("Petroineos.CodingChallenge.IntradayReport.CsvWriter.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Petroineos.CodingChallenge.IntradayReport.CsvWriter
{
    public class CsvReportWriter<TEntity> : IReportWriter<TEntity>
    {
        internal virtual TextWriter GetTextWriter(string fullPath) => new StreamWriter(fullPath);
        
        public async Task WriteAsync(string? folderPath, string fileName, IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentNullException(nameof(folderPath));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, fileName);

            await using var writer = GetTextWriter(fullPath);
            await using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                await csv.WriteRecordsAsync(entities, cancellationToken);
            }
        }
    }
}