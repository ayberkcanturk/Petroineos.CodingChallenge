using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Petroineos.CodingChallenge.IntradayReport.Abstractions;
using Petroineos.CodingChallenge.PowerTradeService.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Petroineos.CodingChallenge.IntradayReport.AggregationService.UnitTests")]
[assembly: InternalsVisibleTo("Petroineos.CodingChallenge.IntradayReport.IntegrationTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Petroineos.CodingChallenge.IntradayReport.AggregationService
{

    public class IntradayReportAggregationService : IIntradayReportAggregationService
    {
        private readonly ILogger<IntradayReportAggregationService> _logger;
        private readonly IOptionsMonitor<ReportOptions> _reportOptions;
        private readonly IPowerTradeService _powerTradeService;
        private readonly IReportWriter _reportWriter;

        public IntradayReportAggregationService(ILogger<IntradayReportAggregationService> logger,
            IOptionsMonitor<ReportOptions> reportOptions,
            IPowerTradeService powerTradeService,
            IReportWriter reportWriter)
        {
            _logger = logger;
            _reportOptions = reportOptions ?? throw new ArgumentNullException(nameof(reportOptions));
            _powerTradeService = powerTradeService ?? throw new ArgumentNullException(nameof(powerTradeService));
            _reportWriter = reportWriter ?? throw new ArgumentNullException(nameof(reportWriter));
        }

        internal virtual DateTime GetCurrentDateTime() => DateTime.Now;

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var currentDateTime = GetCurrentDateTime();

            var trades = await _powerTradeService
                .GetTradesAsync(currentDateTime, cancellationToken);

            var aggregatedTrades = trades
                .SelectMany(x => x.Periods)
                .GroupBy(powerPeriod => powerPeriod.Period)
                .Select(powerPeriods => new PowerPeriod()
                {
                    Period = powerPeriods.Key,
                    Volume = powerPeriods.Sum(v => v.Volume),
                })
                .ToList();

            var fileName = $"{currentDateTime:yyyyMMdd_HHmm}.csv";

            await _reportWriter.WriteAsync(_reportOptions.CurrentValue.FolderPath,
                fileName,
                aggregatedTrades,
                cancellationToken);
        }
    }
}