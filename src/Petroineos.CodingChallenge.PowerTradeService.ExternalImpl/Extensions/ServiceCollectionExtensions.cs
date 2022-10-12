using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Petroineos.CodingChallenge.PowerTradeService.Abstractions;
using Services;

namespace Petroineos.CodingChallenge.PowerTradeService.ExternalImpl.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UsePowerTradeServiceExternalImpl(this IServiceCollection services)
        {
            services.UsePowerServiceExternalLib();
            services.UseMapster();
            services.AddTransient<IPowerTradeService, PowerTradeService>();

            return services;
        }

        internal static IServiceCollection UsePowerServiceExternalLib(this IServiceCollection services)
        {
            services.AddTransient<IPowerService, PowerService>();

            return services;
        }

        internal static IServiceCollection UseMapster(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            services.AddSingleton(config);
            services.AddTransient<IMapper, ServiceMapper>();

            return services;
        }
    }
}
