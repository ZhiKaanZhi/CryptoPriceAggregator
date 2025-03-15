using System.ComponentModel.DataAnnotations;

namespace CryptoPriceAggregator.Models;

public class PriceRecord
{
    [Key]
    public DateTime TimePoint { get; set; }  // Hour-accuracy
    public double AggregatedPrice { get; set; }
}