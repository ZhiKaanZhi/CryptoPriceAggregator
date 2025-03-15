using CryptoPriceAggregator.Models;
using CryptoPriceAggregator.Providers.Interfaces;
using CryptoPriceAggregator.Repositories.Interfaces;
using CryptoPriceAggregator.Services.Implementations;
using Moq;
using Xunit;

namespace CryptoPriceAggregator.Tests;

public class PriceServiceTests
{
    private readonly Mock<IPriceRepository> _priceRepositoryMock;
    private readonly Mock<IPriceProvider> _priceProviderMock;
    private readonly PriceService _priceService;

    public PriceServiceTests()
    {
        _priceRepositoryMock = new Mock<IPriceRepository>();
        _priceProviderMock = new Mock<IPriceProvider>();
        var loggerMock = new Mock<ILogger<PriceService>>();
        _priceService = new PriceService(
            _priceRepositoryMock.Object,
            new List<IPriceProvider> { _priceProviderMock.Object },
            loggerMock.Object);
    }

    [Fact]
    public async Task GetAggregatedPrice_ReturnsCachedPrice_WhenExists()
    {
        // Arrange
        var timePoint = DateTime.UtcNow;
        timePoint = timePoint.Date.AddHours(timePoint.Hour);
        var cachedPrice = new PriceRecord { TimePoint = timePoint, AggregatedPrice = 50000 };
        _priceRepositoryMock.Setup(repo => repo.GetPriceByTimePoint(timePoint)).ReturnsAsync(cachedPrice);

        // Act
        var result = await _priceService.GetAggregatedPrice(timePoint);

        // Assert
        Assert.Equal(50000, result);
        _priceProviderMock.Verify(p => p.GetPriceAsync(It.IsAny<DateTime>()), Times.Never);
    }

    [Fact]
    public async Task GetAggregatedPrice_FetchesAndCaches_WhenNotCached()
    {
        // Arrange
        var timePoint = DateTime.UtcNow;
        timePoint = timePoint.Date.AddHours(timePoint.Hour);
        const string provider = "TestProvider";
        var cachedPrice = new PriceDto { Provider = provider, Price = 50500 };
        _priceRepositoryMock.Setup(repo => repo.GetPriceByTimePoint(timePoint)).ReturnsAsync((PriceRecord)null);
        _priceProviderMock.Setup(p => p.GetPriceAsync(timePoint)).ReturnsAsync(cachedPrice);

        // Act
        var result = await _priceService.GetAggregatedPrice(timePoint);

        // Assert
        Assert.Equal(50500, result);
        _priceRepositoryMock.Verify(repo => repo.AddPrice(It.IsAny<PriceRecord>()), Times.Once);
    }

    [Fact]
    public async Task GetAggregatedPrice_ProvidersReturnNullPrice_WhenNotCached()
    {
        // Arrange
        var timePoint = DateTime.UtcNow;
        timePoint = timePoint.Date.AddHours(timePoint.Hour);
        const string provider = "TestProvider";
        var cachedPrice = new PriceDto { Provider = provider, Price = null };
        _priceRepositoryMock.Setup(repo => repo.GetPriceByTimePoint(timePoint)).ReturnsAsync((PriceRecord)null);
        _priceProviderMock.Setup(p => p.GetPriceAsync(timePoint)).ReturnsAsync(cachedPrice);
        
        // Act
        var result = await _priceService.GetAggregatedPrice(timePoint);

        // Assert
        Assert.Equal(0, result);
        _priceRepositoryMock.Verify(repo => repo.AddPrice(It.IsAny<PriceRecord>()), Times.Once);
    }
}