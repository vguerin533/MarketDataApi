using Dapper;
using MarketDataApi.Database;
using MarketDataApi.Models.Database;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace MarketDataApi.Services
{
    public class TickersService : ITickersService
    {
        private string _connectionString;

        public TickersService(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task InsertAsync(IEnumerable<TickerDbEntity> tickers)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                foreach (var element in tickers)
                {
                    await using var importer = connection.BeginBinaryImport(
                        "COPY mda.tickers (Exchange, Timestamp, VolumeUsd, Volume, PriceChange, Low, High, State, SettlementPrice, OpenInterest, MinPrice, MaxPrice, MarkPrice, " +
                        "LastPrice, InstrumentName, IndexPrice, Funding8H, EstimatedDeliveryPrice, CurrentFunding, BestBidPrice, BestBidAmount, BestAskPrice, BestAskAmount) FROM STDIN (FORMAT binary)");
                    await importer.StartRowAsync();
                    await importer.WriteAsync(element.Exchange, NpgsqlDbType.Varchar);
                    await importer.WriteAsync(element.Timestamp, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.VolumeUsd, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.Volume, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.PriceChange, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.Low, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.High, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.State, NpgsqlDbType.Varchar);
                    await importer.WriteAsync(element.SettlementPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.OpenInterest, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.MinPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.MaxPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.MarkPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.LastPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.InstrumentName, NpgsqlDbType.Varchar);
                    await importer.WriteAsync(element.IndexPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.Funding8H, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.EstimatedDeliveryPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.CurrentFunding, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.BestBidPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.BestBidAmount, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.BestAskPrice, NpgsqlDbType.Numeric);
                    await importer.WriteAsync(element.BestAskAmount, NpgsqlDbType.Numeric);

                    await importer.CompleteAsync();
                }

                await connection.CloseAsync();
            }
        }

        public IEnumerable<TickerDbEntity> GetTickers(string exchange, string instrument)
        {
            var parameters = new { Exchange = exchange, Instrument = instrument };
            IEnumerable<TickerDbEntity> tickers = new List<TickerDbEntity>();
            var sql = Queries.SelectTickers;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                tickers = connection.Query<TickerDbEntity>(sql, parameters);
            }
            return tickers;
        }
    }
}