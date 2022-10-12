using Mapster;
using Microsoft.Extensions.Logging;
using Petroineos.CodingChallenge.PowerTradeService.Abstractions;
using Services;
using PowerTrade = Petroineos.CodingChallenge.PowerTradeService.Abstractions.PowerTrade;

namespace Petroineos.CodingChallenge.PowerTradeService.ExternalImpl
{
    public class PowerTradeService : IPowerTradeService
    {
        private readonly ILogger<PowerTradeService> _logger;
        private readonly IPowerService _powerService;

        public PowerTradeService(ILogger<PowerTradeService> logger,
            IPowerService powerService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _powerService = powerService ?? throw new ArgumentNullException(nameof(powerService));
        }

        public async Task<IEnumerable<PowerTrade>> GetTradesAsync(DateTime date, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogTrace("GetTradesAsync execution started with args: {date}", date);

                _logger.LogInformation("Fetching trades for {date}", date);

                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException(cancellationToken);

                var trades = await _powerService.GetTradesAsync(date);

                return trades.Adapt<IEnumerable<PowerTrade>>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching trades with args: {date}", date);

                throw;
            }
            finally
            {
                _logger.LogTrace("GetTradesAsync execution completed with args: {date}", date);
            }
        }
    }
}