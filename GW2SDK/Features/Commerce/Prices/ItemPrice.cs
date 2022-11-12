using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Prices;

[PublicAPI]
[DataTransferObject]
public sealed record ItemPrice
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>Indicates whether a free to play account can trade this item on the trading post.</summary>
    public required bool Whitelisted { get; init; }

    /// <summary>The highest price someone is willing to buy the item for.</summary>
    public required Coin BestBid { get; init; }

    /// <summary>The lowest price someone is willing to sell the item for.</summary>
    public required Coin BestAsk { get; init; }

    public Coin BidAskSpread =>
        this switch
        {
            { BestAsk.Amount: 0 } => Coin.Zero,
            { BestBid.Amount: 0 } => Coin.Zero,
            _ => BestAsk - BestBid
        };

    /// <summary>The total number of items demanded (regardless of price level).</summary>
    public required int TotalDemand { get; init; }

    /// <summary>The total number of items supplied (regardless of price level).</summary>
    public required int TotalSupply { get; init; }
}
