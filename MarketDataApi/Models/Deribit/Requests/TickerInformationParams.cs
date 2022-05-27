using Newtonsoft.Json;

namespace MarketDataApi.Models.Deribit.Requests
{
    public class TickerInformationParams : IDeribitParams
    {
        [JsonProperty("channels")]
        public List<string> Channels { get; set; }
    }
}
