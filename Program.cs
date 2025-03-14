using CryptoPriceAggregator.Data;
using CryptoPriceAggregator.Providers;
using CryptoPriceAggregator.Repositories;
using CryptoPriceAggregator.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// use In-Memory DB
builder.Services.AddDbContext<PriceDbContext>(options =>
    options.UseInMemoryDatabase("PriceDb"));

// Register Repository & Services with DI
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IPriceService, PriceService>();

// Register external price providers dynamically
builder.Services.AddHttpClient();
builder.Services.AddScoped<IPriceProvider, BitstampPriceProvider>();
builder.Services.AddScoped<IPriceProvider, BitfinexPriceProvider>();

// Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();
