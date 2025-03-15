using CryptoPriceAggregator.Services;
using CryptoPriceAggregator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPriceAggregator.Controllers;

[ApiController]
[Route("api/prices")]
public class PricesController : ControllerBase
{
    private readonly IPriceService _priceService;
    private readonly ILogger<PricesController> _logger;

    public PricesController(
        IPriceService priceService,
        ILogger<PricesController> logger)
    {
        _priceService = priceService;
        _logger = logger;
    }

    [HttpGet("{time}")]
    public async Task<IActionResult> GetAggregatedPrice(DateTime time)
    {
        try
        {
            var price = await _priceService.GetAggregatedPrice(time);
            return Ok(price);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching aggregated price");
            return StatusCode(500, "Internal server error");
        }
    }
}