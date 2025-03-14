using CryptoPriceAggregator.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoPriceAggregator.Data;

public class PriceDbContext : DbContext
{
    public DbSet<PriceRecord> Prices { get; set; }

    public PriceDbContext(DbContextOptions<PriceDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceRecord>()
            .HasIndex(p => p.TimePoint)
            .IsUnique();
    }
}