using CryptoPriceAggregator.Models;
using CryptoPriceAggregator.Providers.Interfaces;
using CryptoPriceAggregator.Repositories.Interfaces;
using CryptoPriceAggregator.Services.Interfaces;

namespace CryptoPriceAggregator.Services.Implementations;

public class PriceService : IPriceService
{
    private readonly IPriceRepository _priceRepository;
    private readonly IEnumerable<IPriceProvider> _priceProviders;
    private readonly IFormulaService _formulaService;
    private readonly ILogger<PriceService> _logger;

    public PriceService(IPriceRepository priceRepository, 
        IEnumerable<IPriceProvider> priceProviders, 
        ILogger<PriceService> logger, 
        IFormulaService formulaService)
    {
        _priceRepository = priceRepository;
        _priceProviders = priceProviders;
        _logger = logger;
        _formulaService = formulaService;
    }

    public async Task<double?> GetAggregatedPrice(DateTime timePoint)
    {
        timePoint = timePoint.Date.AddHours(timePoint.Hour);

        var cachedPrice = await _priceRepository.GetPriceByTimePoint(timePoint);
        if (cachedPrice != null)
        {
            return cachedPrice.AggregatedPrice;
        }

        // Fetch prices from all registered providers
        var tasks = _priceProviders.Select(p => p.GetPriceAsync(timePoint));
        var results = await Task.WhenAll(tasks);

        var prices = results
            .Where(p => 
            {
                if (p.Price == null)
                {
                    _logger.LogWarning("Price for {Provider} at {TimePoint} is null", p.Provider, timePoint);
                    return false; 
                }
                return true;
            })
            .Select(p => p.Price!.Value) 
            .ToList();
        
        var aggregatedPrice = _formulaService.AveragePrice(prices);

        // Save result in the database for caching
        var priceRecord = new PriceRecord
        {
            TimePoint = timePoint,
            AggregatedPrice = aggregatedPrice
        };

        await _priceRepository.AddPrice(priceRecord);
        return aggregatedPrice;
    }
}