namespace Petroineos.CodingChallenge.PowerTradeService.Abstractions
{
    public class PowerTrade
    {
        public DateTime Date { get; set; }
        public PowerPeriod[] Periods { get; set; } = Array.Empty<PowerPeriod>();
    }
}
