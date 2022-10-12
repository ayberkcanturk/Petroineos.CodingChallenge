using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Petroineos.CodingChallenge.IntradayReport.AggregationService.Extensions;
using Petroineos.CodingChallenge.IntradayReport.CsvWriter.Extensions;
using Petroineos.CodingChallenge.PowerTradeService.ExternalImpl.Extensions;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace Petroineos.CodingChallenge.IntradayReport.IntegrationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.UsePowerTradeServiceExternalImpl();
            services.UseCsvReportWriter();
            services.UseIntradayAggregationService();
        }

        public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor) =>
            loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));
    }
}
