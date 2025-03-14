using CryptoPriceAggregator.Models;

namespace CryptoPriceAggregator.Repositories;

public interface IPriceRepository
{
    Task<PriceRecord?> GetPriceByTimePoint(DateTime timestamp);
    Task AddPrice(PriceRecord price);
    Task<IQueryable<PriceRecord>> GetPricesInRange(DateTime start, DateTime end);
}