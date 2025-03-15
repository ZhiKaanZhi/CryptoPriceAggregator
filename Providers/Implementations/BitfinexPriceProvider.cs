using CryptoPriceAggregator.Models;
using CryptoPriceAggregator.Providers.Interfaces;
using Newtonsoft.Json.Linq;

namespace CryptoPriceAggregator.Providers.Implementations;

public class BitfinexPriceProvider : IPriceProvider
{
    public string Provider { get; set; } = "Bitfinex";
    private const string Url = "https://api-pub.bitfinex.com/v2/candles/trade:1h:tBTCUSD/hist?start=";
    
    private readonly HttpClient _httpClient;

    public BitfinexPriceProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PriceDto> GetPriceAsync(DateTime timePoint)
    {
        var unixTimestamp = new DateTimeOffset(timePoint).ToUnixTimeMilliseconds();
        var url = Url+$"{unixTimestamp}&end={unixTimestamp + 3600000}&limit=1";

        var response = await _httpClient.GetStringAsync(url);
        var jsonArray = JArray.Parse(response);
        return new PriceDto()
        {
            Provider = Provider,
            Price = jsonArray.Any() ? jsonArray[0][3].Value<double>() : null
        };
    }
}