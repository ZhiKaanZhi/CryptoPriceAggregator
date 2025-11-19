using System.ComponentModel.DataAnnotations;

namespace CryptoPriceAggregator.Models;

public class PriceRecordDbModel
{
    [Key]
    public int PriceRecordId { get; set; }
    public DateTime TimePoint { get; set; }  // Hour-accuracy
    public double AggregatedPrice { get; set; }
}