namespace Petroineos.CodingChallenge.PowerTradeService.Abstractions
{
    public interface IPowerTradeService
    {
        Task<IEnumerable<PowerTrade>> GetTradesAsync(DateTime date, CancellationToken cancellationToken);
    }
}
