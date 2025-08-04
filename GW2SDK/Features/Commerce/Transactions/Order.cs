namespace GuildWars2.Commerce.Transactions;

/// <summary>Information about the current order.</summary>
[DataTransferObject]
public sealed record Order
{
    /// <summary>The order ID.</summary>
    public required long Id { get; init; }

    /// <summary>The item ID of the order.</summary>
    public required int ItemId { get; init; }

    /// <summary>The unit price per item.</summary>
    public required Coin UnitPrice { get; init; }

    /// <summary>The amount of items ordered.</summary>
    public required int Quantity { get; init; }

    /// <summary>The date and time when the order was created.</summary>
    public required DateTimeOffset Created { get; init; }
}
