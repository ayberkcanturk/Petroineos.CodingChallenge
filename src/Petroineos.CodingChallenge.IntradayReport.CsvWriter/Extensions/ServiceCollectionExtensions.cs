using Microsoft.Extensions.DependencyInjection;
using Petroineos.CodingChallenge.IntradayReport.Abstractions;

namespace Petroineos.CodingChallenge.IntradayReport.CsvWriter.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseCsvReportWriter(this IServiceCollection services)
        {
            services.AddTransient<IReportWriter, CsvReportWriter>();

            return services;
        }
    }
}
