namespace Petroineos.CodingChallenge.IntradayReport.WindowsService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseWorkerService(this IServiceCollection services, IConfigurationRoot? configurationRoot)
        {
            services.Configure<WorkerServiceOptions>(configurationRoot?.GetSection(nameof(WorkerServiceOptions)));
            services.AddHostedService<Worker>();

            return services;
        }
    }
}
