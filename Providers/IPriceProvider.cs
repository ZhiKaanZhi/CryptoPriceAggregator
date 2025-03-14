namespace CryptoPriceAggregator.Providers;

public interface IPriceProvider
{
    string ProviderName { get; }
    
    Task<double?> GetPriceAsync(DateTime timePoint);
}