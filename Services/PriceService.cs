using CryptoPriceAggregator.Models;
using CryptoPriceAggregator.Providers;
using CryptoPriceAggregator.Repositories;

namespace CryptoPriceAggregator.Services;

public class PriceService : IPriceService
{
    private readonly IPriceRepository _priceRepository;
    private readonly IEnumerable<IPriceProvider> _priceProviders;

    public PriceService(IPriceRepository priceRepository, IEnumerable<IPriceProvider> priceProviders)
    {
        _priceRepository = priceRepository;
        _priceProviders = priceProviders;
    }

    public async Task<double> GetAggregatedPrice(DateTime timePoint)
    {
        timePoint = timePoint.Date.AddHours(timePoint.Hour);

        var cachedPrice = await _priceRepository.GetPriceByTimePoint(timePoint);
        if (cachedPrice != null)
            return cachedPrice.AggregatedPrice;

        // Fetch prices from all registered providers
        var tasks = _priceProviders.Select(p => p.GetPriceAsync(timePoint));
        var results = await Task.WhenAll(tasks);
        var prices = results.Where(p => p.HasValue).Select(p => p.Value).ToList();

        // Aggregate prices using the average
        var aggregatedPrice = prices.Any() ? prices.Average() : 0;

        // Save result in the database for caching
        var priceRecord = new PriceRecord
        {
            TimePoint = timePoint,
            AggregatedPrice = aggregatedPrice
        };

        await _priceRepository.AddPrice(priceRecord);
        return aggregatedPrice;
    }

    public async Task<IQueryable<PriceRecord>> GetPricesInRange(DateTime start, DateTime end)
    {
        return await _priceRepository.GetPricesInRange(start, end);
    }
}