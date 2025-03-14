using CryptoPriceAggregator.Models;

namespace CryptoPriceAggregator.Services;

public interface IPriceService
{
    Task<double> GetAggregatedPrice(DateTime timePoint);
    Task<IQueryable<PriceRecord>> GetPricesInRange(DateTime start, DateTime end);
}