using Newtonsoft.Json;

namespace MarketDataApi.Models.Deribit.Responses
{
    public class TickerInformationResponse
    {
        [JsonProperty("tickers")]
        public List<string> Channels { get; set; }
    }
}