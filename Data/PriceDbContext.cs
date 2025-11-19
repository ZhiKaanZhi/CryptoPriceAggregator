using CryptoPriceAggregator.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoPriceAggregator.Data;

public class PriceDbContext : DbContext
{
    public DbSet<PriceRecordDbModel> PriceRecords { get; set; }

    public PriceDbContext(DbContextOptions<PriceDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceRecordDbModel>()
            .HasIndex(p => p.PriceRecordId)
            .IsUnique();
    }
}