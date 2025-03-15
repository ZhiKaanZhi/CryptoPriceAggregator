namespace CryptoPriceAggregator.Models;

public class PriceRecord
{
    public DateTime TimePoint { get; set; }  // Hour-accuracy
    public double AggregatedPrice { get; set; }
}