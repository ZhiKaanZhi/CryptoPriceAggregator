namespace CryptoPriceAggregator.Services.Interfaces;

public interface IPriceService
{
    Task<double?> GetAggregatedPrice(DateTime timePoint);
}