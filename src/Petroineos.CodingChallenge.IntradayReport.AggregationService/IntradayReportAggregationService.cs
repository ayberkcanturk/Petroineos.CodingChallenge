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
        private readonly IReportWriter<PowerTradeReport> _reportWriter;

        public IntradayReportAggregationService(ILogger<IntradayReportAggregationService> logger,
            IOptionsMonitor<ReportOptions> reportOptions,
            IPowerTradeService powerTradeService,
            IReportWriter<PowerTradeReport> reportWriter)
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
                .Select(powerPeriods => new PowerTradeReport(CalculateReportTimeFromPeriod(powerPeriods.Key), powerPeriods.Sum(v => v.Volume)))
                .ToList();

            var fileName = $"{currentDateTime:yyyyMMdd_HHmm}.csv";

            await _reportWriter.WriteAsync(_reportOptions.CurrentValue.FolderPath,
                fileName,
                aggregatedTrades,
                cancellationToken);
        }

        internal static string CalculateReportTimeFromPeriod(int period)
        {
            if (period == 24)
                period = 00;

            var timeSpan = TimeSpan.Parse($"{period}:00").Subtract(new TimeSpan(2, 0, 0));

            if (timeSpan < TimeSpan.Parse("00:00"))
                timeSpan = timeSpan.Add(new TimeSpan(24,00,00));

            return $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}";
        }
    }
}