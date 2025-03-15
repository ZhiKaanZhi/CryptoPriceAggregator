using CryptoPriceAggregator.Models;
using CryptoPriceAggregator.Providers.Interfaces;
using Newtonsoft.Json.Linq;

namespace CryptoPriceAggregator.Providers.Implementations;

public class BitstampPriceProvider :  IPriceProvider
{
    public string Provider { get; set; } = "Bitstamp";
    private const string Url = "https://www.bitstamp.net/api/v2/ohlc/btcusd/?step=3600&limit=1&start=";

    private readonly HttpClient _httpClient;

    public BitstampPriceProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<PriceDto> GetPriceAsync(DateTime timePoint)
    {
        var unixTimestamp = new DateTimeOffset(timePoint).ToUnixTimeSeconds();
        var url = Url + $"{unixTimestamp}";

        var response = await _httpClient.GetStringAsync(url);
        var json = JObject.Parse(response);
        return new PriceDto()
        {
            Provider = Provider,
            Price = json["data"]?["ohlc"]?.First()?["close"]?.Value<double>()
        };
    }
}
