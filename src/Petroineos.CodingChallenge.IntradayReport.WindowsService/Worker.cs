using Microsoft.Extensions.Options;
using Petroineos.CodingChallenge.IntradayReport.Abstractions;
using Polly;

namespace Petroineos.CodingChallenge.IntradayReport.WindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptionsMonitor<WorkerServiceOptions> _workerServiceOptionsMonitor;
        private readonly IIntradayReportAggregationService _intradayReportAggregationService;

        public Worker(ILogger<Worker> logger, IOptionsMonitor<WorkerServiceOptions> workerServiceOptionsMonitor, IIntradayReportAggregationService intradayReportAggregationService)
        {
            _logger = logger;
            _workerServiceOptionsMonitor = workerServiceOptionsMonitor ??
                                           throw new ArgumentNullException(nameof(workerServiceOptionsMonitor));
            _intradayReportAggregationService = intradayReportAggregationService ?? throw new ArgumentNullException(nameof(intradayReportAggregationService));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    await Task.WhenAll(ExecuteWithWaitAndRetry(cancellationToken),
                         Task.Delay(TimeSpan.FromSeconds(_workerServiceOptionsMonitor.CurrentValue.TaskDelayInSeconds), cancellationToken));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);

                Environment.Exit(1);
            }
        }

        private async Task ExecuteWithWaitAndRetry(CancellationToken cancellationToken)
        {
            var waitAndRetry = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount: _workerServiceOptionsMonitor.CurrentValue.NumOfRetries,
                    sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(60 * Math.Pow(2, retryAttempt - 1)),
                    onRetry: (ex, timeSpan, retryAttempt, context) =>
                    {
                        _logger.LogError(ex, "Retry attempt: {retryAttempt}. Operation failed: {ex.Message}", retryAttempt, ex.Message);
                    }
                );

            await waitAndRetry.ExecuteAsync(async () =>
                await _intradayReportAggregationService.ExecuteAsync(cancellationToken));
        }
    }
}