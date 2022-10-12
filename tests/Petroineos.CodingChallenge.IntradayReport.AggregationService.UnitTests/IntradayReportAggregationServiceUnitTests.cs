using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Petroineos.CodingChallenge.IntradayReport.Abstractions;
using Petroineos.CodingChallenge.PowerTradeService.Abstractions;
using Xunit;

namespace Petroineos.CodingChallenge.IntradayReport.AggregationService.UnitTests
{
    public class IntradayReportAggregationServiceUnitTests
    {
        [Fact]
        public async Task VerifyAggregationServiceCallsPowerTradeServiceAndReportWriterWithParameters()
        {
            var currentDateTime = new DateTime(2015, 04, 01, 23, 00, 00);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CsvFiles");
            var fileName = $"{currentDateTime:yyyyMMdd_HHmm}.csv";
            var cancellationToken = new CancellationToken(false);

            var loggerMock = Moq.MockFactory.GetILogger<IntradayReportAggregationService>();
            var reportOptionsMock = new Mock<IOptionsMonitor<ReportOptions>>();
            reportOptionsMock.Setup(r => r.CurrentValue).Returns(new ReportOptions()
            {
                FolderPath = folderPath
            });

            var powerTradeServiceMock = new Mock<IPowerTradeService>();
            powerTradeServiceMock.Setup(p => p.GetTradesAsync(It.IsAny<DateTime>(), new CancellationToken(false)))
                .ReturnsAsync(new List<PowerTrade>());

            var reportWriterMock = new Mock<IReportWriter>();
            reportWriterMock.Setup(r => r.WriteAsync(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<IEnumerable<PowerTrade>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var intradayReportAggregationService = new Mock<IntradayReportAggregationService>(loggerMock.Object,
                reportOptionsMock.Object,
                powerTradeServiceMock.Object,
                reportWriterMock.Object);

            intradayReportAggregationService.Setup(s => s.GetCurrentDateTime()).Returns(currentDateTime);

            await intradayReportAggregationService.Object.ExecuteAsync(cancellationToken);

            powerTradeServiceMock.Verify(r => r.GetTradesAsync(currentDateTime, cancellationToken), Times.Once);
            reportWriterMock.Verify(r=>r.WriteAsync(folderPath, fileName, It.IsAny<IEnumerable<PowerPeriod>>(), cancellationToken), Times.Once);
        }
    }
}