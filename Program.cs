using CryptoPriceAggregator.Data;
using CryptoPriceAggregator.Providers.Implementations;
using CryptoPriceAggregator.Providers.Interfaces;
using CryptoPriceAggregator.Repositories.Implementations;
using CryptoPriceAggregator.Repositories.Interfaces;
using CryptoPriceAggregator.Services.Implementations;
using CryptoPriceAggregator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var applicationUrl = builder.Configuration.GetValue<string>("applicationUrl");
builder.WebHost.UseUrls(applicationUrl);

// use In-Memory DB
builder.Services.AddDbContext<PriceDbContext>(options =>
    options.UseInMemoryDatabase("PriceDb"));

// Register Repository & Services
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IFormulaService, FormulaService>();

// Register price providers
builder.Services.AddHttpClient();
builder.Services.AddScoped<IPriceProvider, BitstampPriceProvider>();
builder.Services.AddScoped<IPriceProvider, BitfinexPriceProvider>();

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
