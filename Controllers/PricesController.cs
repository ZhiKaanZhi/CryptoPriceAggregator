using CryptoPriceAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPriceAggregator.Controllers;

[ApiController]
[Route("api/prices")]
public class PricesController : ControllerBase
{
    private readonly IPriceService _priceService;

    public PricesController(IPriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpGet("{time}")]
    public async Task<IActionResult> GetAggregatedPrice(DateTime time)
    {
        var price = await _priceService.GetAggregatedPrice(time);
        return Ok(price);
    }
}