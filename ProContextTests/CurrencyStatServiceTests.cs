using Moq;
using ProContext.Dtos;
using ProContext.Services;

namespace ProContextTests;

public class CurrencyStatServiceTests
{
    [Test]
	public async Task TestGetStats()
	{
        var mockProvider = new Mock<ICbrCurrencyProvider>();
        mockProvider.Setup(provider => provider.GetRatesByDate(It.IsAny<DateTime>()))
                    .ReturnsAsync(new List<CbrCurrency>
                    {
                        new CbrCurrency { CharCode = "USD", Value = "75.50" },
                    });

        var service = new CurrencyStatService(mockProvider.Object);
        var stats = await service.GetStats();

        Assert.That(stats, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task TestGetStatsHttpException()
    {
        var mockProvider = new Mock<ICbrCurrencyProvider>();
        mockProvider.Setup(provider => provider.GetRatesByDate(It.IsAny<DateTime>()))
                    .ThrowsAsync(new HttpRequestException("Failed to fetch data"));

        var service = new CurrencyStatService(mockProvider.Object);
        var stats = await service.GetStats();

        Assert.That(stats, Has.Count.EqualTo(0));
    }
}

