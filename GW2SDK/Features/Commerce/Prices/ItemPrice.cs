namespace GuildWars2.Commerce.Prices;

/// <summary>Information about the best prices for an item on the trading post.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record ItemPrice
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>Indicates whether a free to play account can trade this item on the trading post.</summary>
    public required bool Whitelisted { get; init; }

    /// <summary>The highest bid price on the demand side.</summary>
    public required Coin BestBid { get; init; }

    /// <summary>The lowest ask price on the supply side.</summary>
    public required Coin BestAsk { get; init; }

    /// <summary>The difference between the lowest ask price on the supply side and the highest bid price on the demand side.</summary>
    public Coin BidAskSpread =>
        this switch
        {
            { BestAsk.Amount: 0 } => Coin.Zero,
            { BestBid.Amount: 0 } => Coin.Zero,
            _ => BestAsk - BestBid
        };

    /// <summary>The total number of items demanded, irrespective of price.</summary>
    public required int TotalDemand { get; init; }

    /// <summary>The total number of items supplied, irrespective of price.</summary>
    public required int TotalSupply { get; init; }
}
