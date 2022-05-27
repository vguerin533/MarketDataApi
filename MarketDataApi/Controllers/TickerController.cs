using System.Net;
using MarketDataApi.Models.Database;
using MarketDataApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarketDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TickerController : ControllerBase
    {
        private readonly ILogger<DeribitSubscriptionsController> _logger;
        private readonly ITickersService _tickersService;

        public TickerController(ILogger<DeribitSubscriptionsController> logger, ITickersService tickersService)
        {
            _logger = logger;
            _tickersService = tickersService;
        }

        [HttpGet(Name = "Get")]
        [SwaggerOperation(Tags = new[] { "Tickers" })]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(IEnumerable<TickerDbEntity>))]
        public IEnumerable<TickerDbEntity> Get([FromQuery] int? numberOfTickers, [FromQuery] string exchange = "Deribit", [FromQuery] string intrumentName = "BTC-PERPETUAL")
        {
            var tickers = _tickersService.GetTickers(exchange, intrumentName);

            if (numberOfTickers.HasValue)
            {
                tickers = tickers.Take(numberOfTickers.Value);
            }

            return tickers;
        }
    }
}
