using Newtonsoft.Json;

namespace MarketDataApi.Models.Deribit.Requests
{
    public class AuthenticationParams : IDeribitParams
    {
        [JsonProperty("grant_type")] 
        public string GrantType { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}
