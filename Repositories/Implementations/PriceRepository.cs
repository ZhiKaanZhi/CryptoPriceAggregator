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

    public async Task<PriceRecordDbModel?> GetPriceByTimePoint(DateTime timePoint)
    {
        return await _context.PriceRecords.FirstOrDefaultAsync(p => p.TimePoint == timePoint);
    }

    public async Task AddPrice(PriceRecordDbModel price)
    {
        _context.PriceRecords.Add(price);
        await _context.SaveChangesAsync();
    }
}