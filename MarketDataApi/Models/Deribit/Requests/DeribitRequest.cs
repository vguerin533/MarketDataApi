using Newtonsoft.Json;

namespace MarketDataApi.Models.Deribit.Requests
{
    public class DeribitRequest<T>
        where T : IDeribitParams
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; } = "2.0";

        [JsonProperty("id")]
        public long Id { get; } = Random.Shared.NextInt64();

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")] 
        public T Params { get; set; }
    }
}
