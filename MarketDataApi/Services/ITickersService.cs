using MarketDataApi.Models.Database;

namespace MarketDataApi.Services
{
    public interface ITickersService
    {
        Task InsertAsync(IEnumerable<TickerDbEntity> tickers);

        IEnumerable<TickerDbEntity> GetTickers(string exchange, string instrument);
    }
}
