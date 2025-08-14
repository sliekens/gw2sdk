namespace GuildWars2.Commerce.Listings;

/// <summary>Information about a price level.</summary>
[DataTransferObject]
public sealed record OrderBookLine
{
    /// <summary>The number of listings at this price level.</summary>
    public required int Listings { get; init; }

    /// <summary>The number of items in demand or supply at this price level. This is the sum of the quantities of all listings
    /// at this price level.</summary>
    public required int Quantity { get; init; }

    /// <summary>The price level.</summary>
    public required Coin UnitPrice { get; init; }
}
