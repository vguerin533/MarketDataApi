using System.Collections.Concurrent;
using System.Net.WebSockets;
using MarketDataApi.Config.Deribit;
using MarketDataApi.Models.Deribit;
using MarketDataApi.Services;

namespace MarketDataApi.Clients.Deribit
{
    public class DeribitSubscriptions : IDisposable
    {
        private readonly ConcurrentDictionary<string, DeribitClient> _subscriptionByTickers;
        private readonly DeribitConfig _config;
        private BlockingCollection<DeribitTicker> _tickersCollection;
        private ITickersService _tickersService;

        public DeribitSubscriptions(DeribitConfig config, ITickersService tickersService, BlockingCollection<DeribitTicker> tickersCollection)
        {
            _config = config;
            _subscriptionByTickers = new ConcurrentDictionary<string, DeribitClient>();
            _tickersCollection = tickersCollection;
            _tickersService = tickersService;
            Task.Factory.StartNew(() => ConsumeAsync(_tickersService));
        }

        public void Dispose()
        {
            _tickersCollection.Dispose();
        }

        public Task GetThenSubscribeFireAndForget(string ticker)
        {
            var client = new DeribitClient(_config, new ClientWebSocket());
            _subscriptionByTickers[ticker] = client;
            return Task.Factory.StartNew(async () =>
            {
                await client.SubscribeAsync(ticker, _tickersCollection);
            });
        }

        public async Task ConsumeAsync(ITickersService _tickersService)
        {
            while (true)
            {
                try
                {
                    var tickers = _tickersCollection.GetConsumingEnumerable();
                    await _tickersService.InsertAsync(tickers.Select(t => t.ToDbEntity()));
                }
                catch (Exception ex)
                {
                    int i = 0;
                }
            }
        }

        public async Task RemoveAsync(string ticker)
        {
            _subscriptionByTickers.Remove(ticker, out var client);
            await client?.UnsubscribeAsync();
        }

        public bool HasKey(string ticker)
        {
            return _subscriptionByTickers.ContainsKey(ticker);
        }
    }
}
