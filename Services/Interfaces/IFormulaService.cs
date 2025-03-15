namespace CryptoPriceAggregator.Services.Interfaces;

public interface IFormulaService
{
    public double AveragePrice(List<double> prices);
}