using CryptoPriceAggregator.Data;
using CryptoPriceAggregator.Models;
using CryptoPriceAggregator.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoPriceAggregator.Repositories.Implementations;

public class PriceRepository : IPriceRepository
{
    private readonly PriceDbContext _context;

    public PriceRepository(PriceDbContext context)
    {
        _context = context;
    }

    public async Task<PriceRecord?> GetPriceByTimePoint(DateTime timePoint)
    {
        return await _context.Prices.FirstOrDefaultAsync(p => p.TimePoint == timePoint);
    }

    public async Task AddPrice(PriceRecord price)
    {
        _context.Prices.Add(price);
        await _context.SaveChangesAsync();
    }
}