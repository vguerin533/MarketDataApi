namespace MarketDataApi.Config.Deribit
{
    public interface IDeribitConfig
    {
        string BaseUrl { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
    }
}
