using System.Collections.Concurrent;
using System.Net.WebSockets;
using MarketDataApi.Clients.Deribit;
using MarketDataApi.Config.Deribit;
using MarketDataApi.Models.Deribit;
using MarketDataApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false)
    .Build();

DeribitConfig deribitConfig = new DeribitConfig();
configuration.Bind("Deribit", deribitConfig);
builder.Services.AddSingleton(deribitConfig);
var connectionString = configuration.GetConnectionString("postgres");
builder.Services.AddTransient<IDeribitClient>(sc => new DeribitClient(sc.GetService<DeribitConfig>(), new ClientWebSocket()));
builder.Services.AddTransient<ITickersService>(sc => new TickersService(connectionString));
builder.Services.AddSingleton(sc => new DeribitSubscriptions(sc.GetService<DeribitConfig>(), sc.GetService<ITickersService>(), new BlockingCollection<DeribitTicker>(100000)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Contact = new OpenApiContact
        {
            Email = "v.guerin533@laposte.net",
            Name = "Vincent Guérin"
        },
        Description = "Market data API that supports Deribit exchange",
        License = new OpenApiLicense
        {
            Name = "MIT License"
        },
        Title = "Market data API",
        Version = "0.1.0"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.EnableValidator(null);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
