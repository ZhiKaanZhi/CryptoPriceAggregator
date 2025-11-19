using CryptoPriceAggregator.Models;

namespace CryptoPriceAggregator.Repositories.Interfaces;

public interface IPriceRepository
{
    Task<PriceRecordDbModel?> GetPriceByTimePoint(DateTime timestamp);
    Task AddPrice(PriceRecordDbModel price);
}