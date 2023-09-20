﻿namespace GuildWars2.Commerce.Listings;

[PublicAPI]
[DataTransferObject]
public sealed record OrderBook
{
    public required int Id { get; init; }

    public required IReadOnlyCollection<OrderBookLine> Demand { get; init; }

    public required IReadOnlyCollection<OrderBookLine> Supply { get; init; }

    /// <summary>The highest price someone is willing to buy the item for.</summary>
    public Coin? BestBid => Demand.FirstOrDefault()?.UnitPrice;

    /// <summary>The lowest price someone is willing to sell the item for.</summary>
    public Coin? BestAsk => Supply.FirstOrDefault()?.UnitPrice;

    public Coin BidAskSpread =>
        this switch
        {
            { BestAsk: null } => Coin.Zero,
            { BestBid: null } => Coin.Zero,
            _ => BestAsk.Value - BestBid.Value
        };

    /// <summary>The total number of items demanded (regardless of price level).</summary>
    public int TotalDemand => Demand.Sum(bid => bid.Quantity);

    /// <summary>The total number of items supplied (regardless of price level).</summary>
    public int TotalSupply => Supply.Sum(ask => ask.Quantity);
}
