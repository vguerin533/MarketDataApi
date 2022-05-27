using System.Net;
using MarketDataApi.Clients.Deribit;
using MarketDataApi.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarketDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeribitSubscriptionsController : ControllerBase
    {
        private readonly ILogger<DeribitSubscriptionsController> _logger;
        private readonly DeribitSubscriptions _subscriptions;

        public DeribitSubscriptionsController(ILogger<DeribitSubscriptionsController> logger, DeribitSubscriptions subscriptions)
        {
            _logger = logger;
            _subscriptions = subscriptions;
        }

        [HttpPost(Name = "SuscribeAsync")]
        [SwaggerOperation(Tags = new[] { "Deribit" })]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<OkObjectResult> SuscribeAsync([FromQuery] string intrumentName = "BTC-PERPETUAL", [FromQuery] string interval = "raw")
        {
            var ticker = $"ticker.{intrumentName}.{interval}";
            _logger.LogInformation($"Asking subscription for ticker {ticker} on deribit");
            if (_subscriptions.HasKey(ticker))
            {
                _logger.LogInformation($"{ticker} already subscribed");
                return Ok($"{ticker} already subscribed");
            }

            _logger.LogInformation($"Starting {ticker} suscription on deribit");
            await _subscriptions.GetThenSubscribeFireAndForget(ticker);
            _logger.LogInformation($"{ticker} suscribed on deribit");

            return Ok($"{ticker} is subscribed");
        }

        [HttpDelete(Name = "UnsuscribeAsync")]
        [SwaggerOperation(Tags = new[] { "Deribit" })]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<OkObjectResult> UnsuscribeAsync(string ticker)
        {
            _logger.LogInformation($"Ending {ticker} suscription on deribit");
            await _subscriptions.RemoveAsync(ticker);
            _logger.LogInformation($"{ticker} suscribed on deribit");

            return Ok($"{ticker} suscription is removed");
        }
    }
}
