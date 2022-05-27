CREATE SCHEMA IF NOT EXISTS mda;

CREATE TABLE mda.tickers
(
    id int NOT NULL AUTO_INCREMENT,
	Exchange VARCHAR(100),
	Timestamp DECIMAL,
	VolumeUsd DECIMAL,
	Volume DECIMAL,
	PriceChange DECIMAL,
	Low DECIMAL,
	High DECIMAL,
	State VARCHAR(100),
	SettlementPrice DECIMAL,
	OpenInterest DECIMAL,
	MinPrice DECIMAL,
	MaxPrice DECIMAL,
	MarkPrice DECIMAL,
	LastPrice DECIMAL,
	InstrumentName VARCHAR(100),
	IndexPrice DECIMAL,
	Funding8H DECIMAL,
	EstimatedDeliveryPrice DECIMAL,
	CurrentFunding DECIMAL,
	BestBidPrice DECIMAL,
	BestBidAmount DECIMAL,
	BestAskPrice DECIMAL,
	BestAskAmount DECIMAL
)