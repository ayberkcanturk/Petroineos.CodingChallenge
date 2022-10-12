using CsvHelper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Petroineos.CodingChallenge.IntradayReport.Abstractions;
using Petroineos.CodingChallenge.IntradayReport.AggregationService;
using Petroineos.CodingChallenge.PowerTradeService.Abstractions;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Xunit.DependencyInjection;

namespace Petroineos.CodingChallenge.IntradayReport.IntegrationTests
{
    public class AggregationServiceTests
    {
        [Theory]
        [InlineData(null)]
        public async Task ShouldGenerateCsvReportForGivenDateTime([FromServices] IServiceProvider serviceProvider)
        {
            var currentDateTime = new DateTime(2015, 04, 01, 23, 00, 00);

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CsvFiles");
            var fileName = $"{currentDateTime:yyyyMMdd_HHmm}.csv";
            var fullPath = Path.Combine(folderPath, fileName);

            var logger = serviceProvider.GetRequiredService<ILogger<IntradayReportAggregationService>>();

            var reportOptions = new Mock<IOptionsMonitor<ReportOptions>>();
            reportOptions.Setup(r => r.CurrentValue).Returns(new ReportOptions()
            {
                FolderPath = folderPath
            });

            var powerTradeService = serviceProvider.GetRequiredService<IPowerTradeService>();
            var reportWrite = serviceProvider.GetRequiredService<IReportWriter>();

            var aggregationServiceMock =
                new Mock<IntradayReportAggregationService>(logger, reportOptions.Object, powerTradeService, reportWrite);
            
            aggregationServiceMock.Setup(s => s.GetCurrentDateTime()).Returns(currentDateTime);

            await aggregationServiceMock.Object.ExecuteAsync(new CancellationToken(false));

            var folderExists = Directory.Exists(folderPath);
            folderExists.Should().BeTrue("because it should have created the destination folder.");

            var fileExists = File.Exists(fullPath);
            fileExists.Should().BeTrue("because it should have created a report file.");

            using (var reader = new StreamReader(fullPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PowerPeriod>();
                records.Should().HaveCount(24, "because there are 24 hours in a day.");
            }
        }
    }
}