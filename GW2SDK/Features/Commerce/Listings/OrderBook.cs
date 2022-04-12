using System;
using System.Collections.Generic;
using System.Linq;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings;

[PublicAPI]
[DataTransferObject]
public sealed record OrderBook
{
    public int Id { get; init; }

    public IReadOnlyCollection<OrderBookLine> Demand { get; init; } = Array.Empty<OrderBookLine>();

    public IReadOnlyCollection<OrderBookLine> Supply { get; init; } = Array.Empty<OrderBookLine>();

    /// <summary>The highest price someone is willing to buy the item for.</summary>
    public Coin? BestBid =>
        Demand.FirstOrDefault()
            ?.UnitPrice;

    /// <summary>The lowest price someone is willing to sell the item for.</summary>
    public Coin? BestAsk =>
        Supply.FirstOrDefault()
            ?.UnitPrice;

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