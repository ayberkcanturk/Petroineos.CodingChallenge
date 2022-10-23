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

                    var mainTask = ExecuteWithWaitAndRetry(cancellationToken);
                    var delayerTask = Task.Delay(TimeSpan.FromSeconds(_workerServiceOptionsMonitor.CurrentValue.TaskDelayInSeconds), cancellationToken);

                    try
                    {
                        await Task.WhenAll(mainTask, delayerTask);
                    }
                    catch (AggregateException ae)
                    {
                        var baseException = ae.GetBaseException();

                        foreach (var e in ae.Flatten().InnerExceptions)
                        {
                            var message = e.Message;

                            if (baseException == e)
                            {
                                message = $"Base Exception: {message}";
                            }

                            _logger.LogError(e, "{Message}", message);
                        }
                    }
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
                    _workerServiceOptionsMonitor.CurrentValue.NumOfRetries,
                    retryAttempt => TimeSpan.FromSeconds(60 * Math.Pow(2, retryAttempt)),
                    (ex, timeSpan, retryAttempt, context) =>
                    {
                        _logger.LogError(ex, "Retry attempt: {retryAttempt}. Operation failed: {ex.Message}", retryAttempt, ex.Message);
                    }
                );

            await waitAndRetry.ExecuteAsync(async () =>
                await _intradayReportAggregationService.ExecuteAsync(cancellationToken));
        }
    }
}