using System.Collections.Concurrent;
using MarketDataApi.Models.Deribit;

namespace MarketDataApi.Clients.Deribit
{
    public interface IDeribitClient
    {
        Task SubscribeAsync(string ticker, BlockingCollection<DeribitTicker> tickersCollection);

        Task UnsubscribeAsync();
    }
}
