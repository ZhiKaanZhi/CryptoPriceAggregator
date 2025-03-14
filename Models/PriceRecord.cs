namespace CryptoPriceAggregator.Models;

public class PriceRecord
{
    public int Id { get; set; }
    public DateTime TimePoint { get; set; }  // Hour-accuracy
    public double AggregatedPrice { get; set; }
}