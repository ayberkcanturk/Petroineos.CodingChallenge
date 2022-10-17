using System.Reflection;
using Petroineos.CodingChallenge.IntradayReport.AggregationService.Extensions;
using Petroineos.CodingChallenge.IntradayReport.CsvWriter.Extensions;
using Petroineos.CodingChallenge.PowerTradeService.ExternalImpl.Extensions;
using Serilog;

namespace Petroineos.CodingChallenge.IntradayReport.WindowsService
{
    public class Program
    {
        private static IConfigurationRoot? _configurationRoot;

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("LogFiles/diagnostics.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Petroineos.CodingChallenge starting!");

            IHost host = Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseWindowsService((options) =>
                {
                    options.ServiceName = Assembly.GetExecutingAssembly().FullName;
                })
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    configuration
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                    _configurationRoot = configuration.Build();

                })
                .ConfigureServices(services =>
                {
                    services.UsePowerTradeServiceExternalImpl();
                    services.UseCsvReportWriter();
                    services.UseIntradayAggregationService(_configurationRoot);
                    services.Configure<WorkerServiceOptions>(_configurationRoot?.GetSection(nameof(WorkerServiceOptions)));
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}