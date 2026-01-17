namespace GuildWars2.Commerce.Listings;

/// <summary>Information about the current supply and demand for an item.</summary>
[DataTransferObject]
public sealed record OrderBook
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>The bid prices on the demand side.</summary>
    public required IImmutableValueList<OrderBookLine> Demand { get; init; }

    /// <summary>The ask prices on the supply side.</summary>
    public required IImmutableValueList<OrderBookLine> Supply { get; init; }

    /// <summary>The highest bid price on the demand side.</summary>
    public Coin? BestBid => Demand.Count > 0 ? Demand[0].UnitPrice : null;

    /// <summary>The lowest ask price on the supply side.</summary>
    public Coin? BestAsk => Supply.Count > 0 ? Supply[0].UnitPrice : null;

    /// <summary>The difference between the lowest ask price on the supply side and the highest bid price on the demand side.</summary>
    public Coin BidAskSpread =>
        this switch
        {
            { BestAsk: null } => Coin.Zero,
            { BestBid: null } => Coin.Zero,
            _ => BestAsk.Value - BestBid.Value
        };

    /// <summary>The total number of items demanded, irrespective of price.</summary>
    public int TotalDemand => Demand.Sum(bid => bid.Quantity);

    /// <summary>The total number of items supplied, irrespective of price.</summary>
    public int TotalSupply => Supply.Sum(ask => ask.Quantity);
}
