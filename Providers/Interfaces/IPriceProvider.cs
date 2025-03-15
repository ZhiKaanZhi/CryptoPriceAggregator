using CryptoPriceAggregator.Models;

namespace CryptoPriceAggregator.Providers.Interfaces;

public interface IPriceProvider
{
    Task<PriceDto> GetPriceAsync(DateTime timePoint);
}