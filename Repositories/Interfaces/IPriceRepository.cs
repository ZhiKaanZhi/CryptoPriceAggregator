using CryptoPriceAggregator.Models;

namespace CryptoPriceAggregator.Repositories.Interfaces;

public interface IPriceRepository
{
    Task<PriceRecord?> GetPriceByTimePoint(DateTime timestamp);
    Task AddPrice(PriceRecord price);
}