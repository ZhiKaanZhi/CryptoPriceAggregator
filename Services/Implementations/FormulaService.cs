using CryptoPriceAggregator.Services.Interfaces;

namespace CryptoPriceAggregator.Services.Implementations;

public class FormulaService : IFormulaService
{
    public double AveragePrice(List<double> prices)
    {
        return prices.Count != 0 ? prices.Average() : 0;
    }
}