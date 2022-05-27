using Newtonsoft.Json;

namespace MarketDataApi.Models.Deribit.Responses
{
    public class DeribitResponse<T>
        where T : IDeribitResponse
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; } = "2.0";

        [JsonProperty("id")]
        public long Id { get; } = Random.Shared.NextInt64();

        [JsonProperty("result")] 
        public T Result { get; set; }
    }
}
