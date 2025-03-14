namespace CryptoPriceAggregator.Providers;

public interface IPriceProvider
{
    Task<double?> GetPriceAsync(DateTime timePoint);
}