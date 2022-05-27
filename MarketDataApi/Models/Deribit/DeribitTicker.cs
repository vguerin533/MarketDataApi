using MarketDataApi.Models.Database;
using Newtonsoft.Json;

namespace MarketDataApi.Models.Deribit
{
    public class DeribitTicker
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public Params Params { get; set; }

        public TickerDbEntity ToDbEntity()
        {
            return new TickerDbEntity
            {
                Exchange = "Deribit",
                Timestamp = Params.Data.Timestamp,
                VolumeUsd = Params.Data.Stats.VolumeUsd,
                Volume = Params.Data.Stats.Volume,
                PriceChange = Params.Data.Stats.PriceChange,
                Low = Params.Data.Stats.Low,
                High = Params.Data.Stats.High,
                State = Params.Data.State,
                SettlementPrice = Params.Data.SettlementPrice,
                OpenInterest = Params.Data.OpenInterest,
                MinPrice = Params.Data.MinPrice,
                MaxPrice = Params.Data.MaxPrice,
                MarkPrice = Params.Data.MarkPrice,
                LastPrice = Params.Data.LastPrice,
                InstrumentName = Params.Data.InstrumentName,
                IndexPrice = Params.Data.IndexPrice,
                Funding8H = Params.Data.Funding8H,
                EstimatedDeliveryPrice = Params.Data.EstimatedDeliveryPrice,
                CurrentFunding = Params.Data.CurrentFunding,
                BestBidPrice = Params.Data.BestBidPrice,
                BestBidAmount = Params.Data.BestBidAmount,
                BestAskPrice = Params.Data.BestAskPrice,
                BestAskAmount = Params.Data.BestAskAmount
            };
        }
    }

    public class Params
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("settlement_price")]
        public double SettlementPrice { get; set; }

        [JsonProperty("open_interest")]
        public long OpenInterest { get; set; }

        [JsonProperty("min_price")]
        public double MinPrice { get; set; }

        [JsonProperty("max_price")]
        public double MaxPrice { get; set; }

        [JsonProperty("mark_price")]
        public double MarkPrice { get; set; }

        [JsonProperty("last_price")]
        public double LastPrice { get; set; }

        [JsonProperty("instrument_name")]
        public string InstrumentName { get; set; }

        [JsonProperty("index_price")]
        public double IndexPrice { get; set; }

        [JsonProperty("funding_8h")]
        public double Funding8H { get; set; }

        [JsonProperty("estimated_delivery_price")]
        public double EstimatedDeliveryPrice { get; set; }

        [JsonProperty("current_funding")]
        public double CurrentFunding { get; set; }

        [JsonProperty("best_bid_price")]
        public double BestBidPrice { get; set; }

        [JsonProperty("best_bid_amount")]
        public long BestBidAmount { get; set; }

        [JsonProperty("best_ask_price")]
        public long BestAskPrice { get; set; }

        [JsonProperty("best_ask_amount")]
        public long BestAskAmount { get; set; }
    }

    public class Stats
    {
        [JsonProperty("volume_usd")]
        public long VolumeUsd { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("price_change")]
        public double PriceChange { get; set; }

        [JsonProperty("low")]
        public long Low { get; set; }

        [JsonProperty("high")]
        public long High { get; set; }
    }
}
