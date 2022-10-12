using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Times = Moq.Times;

namespace Petroineos.CodingChallenge.PowerTradeService.ExternalImpl.UnitTests
{
    public class PowerTradeServiceTests
    {

        [Fact]
        public async Task ShouldPowerTradesServiceGetTradesCallPowerServiceGetTradesForOnce()
        {
            var logger = Moq.MockFactory.GetILogger<PowerTradeService>();

            var powerServiceMock = new Mock<IPowerService>();

            powerServiceMock
                .Setup(p => p.GetTradesAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(new List<PowerTrade>());

            var powerTradeServiceMock = new Mock<PowerTradeService>(logger.Object, powerServiceMock.Object);

            DateTime datetime = DateTime.Now;
            CancellationToken cancellationToken = new CancellationToken(false);

            await powerTradeServiceMock.Object.GetTradesAsync(datetime, cancellationToken);

            powerServiceMock.Verify((a) => a.GetTradesAsync(datetime), Times.Once);
        }
    }
}