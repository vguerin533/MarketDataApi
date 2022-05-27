namespace MarketDataApi.Config.Deribit
{
    public class DeribitConfig : IDeribitConfig
    {
        public string BaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
