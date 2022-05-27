using Newtonsoft.Json;

namespace MarketDataApi.Models.Database
{
    public class TickerDbEntity
    {
        [JsonProperty("TickerId")]
        public long TickerId { get; set; }

        [JsonProperty("Exchange")]
        public string Exchange { get; set; }

        [JsonProperty("Timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("VolumeUsd")]
        public long VolumeUsd { get; set; }

        [JsonProperty("Volume")]
        public double Volume { get; set; }

        [JsonProperty("PriceChange")]
        public double PriceChange { get; set; }

        [JsonProperty("Low")]
        public long Low { get; set; }

        [JsonProperty("High")]
        public long High { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("SettlementPrice")]
        public double SettlementPrice { get; set; }

        [JsonProperty("OpenInterest")]
        public long OpenInterest { get; set; }

        [JsonProperty("MinPrice")]
        public double MinPrice { get; set; }

        [JsonProperty("MaxPrice")]
        public double MaxPrice { get; set; }

        [JsonProperty("MarkPrice")]
        public double MarkPrice { get; set; }

        [JsonProperty("LastPrice")]
        public double LastPrice { get; set; }

        [JsonProperty("InstrumentName")]
        public string InstrumentName { get; set; }

        [JsonProperty("IndexPrice")]
        public double IndexPrice { get; set; }

        [JsonProperty("Funding8H")]
        public double Funding8H { get; set; }

        [JsonProperty("EstimatedDeliveryPrice")]
        public double EstimatedDeliveryPrice { get; set; }

        [JsonProperty("CurrentFunding")]
        public double CurrentFunding { get; set; }

        [JsonProperty("BestBidPrice")]
        public double BestBidPrice { get; set; }

        [JsonProperty("BestBidAmount")]
        public long BestBidAmount { get; set; }

        [JsonProperty("BestAskPrice")]
        public long BestAskPrice { get; set; }

        [JsonProperty("BestAskAmount")]
        public long BestAskAmount { get; set; }
    }
}
