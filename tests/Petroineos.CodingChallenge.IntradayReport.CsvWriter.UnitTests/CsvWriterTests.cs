using FluentAssertions;
using Moq;
using Petroineos.CodingChallenge.IntradayReport.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Petroineos.CodingChallenge.IntradayReport.CsvWriter.UnitTests
{
    public class CsvWriterTests
    {
        [Fact]
        public async Task ShouldThrowExceptionOnNotExpectedNullArguments()
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CsvFiles");
            const string fileName = $"test.csv";
            var cancellationToken = new CancellationToken(false);
            var entities = new List<PowerTradeReport>();

            var reportWriterMock = new Mock<CsvReportWriter<PowerTradeReport>>();

            Func<Task> folderPathNull = async () => await reportWriterMock.Object.WriteAsync(null, fileName, entities, cancellationToken);
            await folderPathNull.Should().ThrowAsync<ArgumentNullException>();

            Func<Task> fileNameNull = async () => await reportWriterMock.Object.WriteAsync(folderPath, null, entities, cancellationToken);
            await fileNameNull.Should().ThrowAsync<ArgumentNullException>();

            entities = null;

            Func<Task> entitiesNull = async () => await reportWriterMock.Object.WriteAsync(folderPath, fileName, entities, cancellationToken);
            await entitiesNull.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ShouldCsvContainsCorrectInformation()
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CsvFiles");
            const string fileName = $"test.csv";
            var cancellationToken = new CancellationToken(false);
            var entities = new List<PowerTradeReport>()
            {
                new("1:00", 12.00)
            };

            var reportWriterMock = new Mock<CsvReportWriter<PowerTradeReport>>();
            var writer = new StringWriter();

            reportWriterMock.Setup(r => r.GetTextWriter(It.IsAny<string>()))
                .Returns(writer);

            await reportWriterMock.Object.WriteAsync(folderPath, fileName, entities, cancellationToken);

            writer.ToString().Should().Be("Time,Volume\r\n1:00,12\r\n");
        }
    }
}