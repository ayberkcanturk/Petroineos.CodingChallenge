using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petroineos.CodingChallenge.IntradayReport.Abstractions;

namespace Petroineos.CodingChallenge.IntradayReport.AggregationService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseIntradayAggregationService(this IServiceCollection services, IConfigurationRoot? configurationRoot = null)
        {
            if (configurationRoot != null)
                services.Configure<ReportOptions>(configurationRoot.GetSection(nameof(ReportOptions)));

            services.AddTransient<IIntradayReportAggregationService, IntradayReportAggregationService>();

            return services;
        }
    }
}
