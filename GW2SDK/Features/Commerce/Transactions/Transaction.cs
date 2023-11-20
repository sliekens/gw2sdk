namespace GuildWars2.Commerce.Transactions;

/// <summary>Information about the completed transaction.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Transaction
{
    /// <summary>The order ID.</summary>
    public required long Id { get; init; }

    /// <summary>The item ID of the transaction.</summary>
    public required int ItemId { get; init; }

    /// <summary>The price per item.</summary>
    public required Coin UnitPrice { get; init; }

    /// <summary>The amount of items in the transaction.</summary>
    public required int Quantity { get; init; }

    /// <summary>The date and time when the order was created.</summary>
    public required DateTimeOffset Created { get; init; }

    /// <summary>The date and time when the order was completed.</summary>
    public required DateTimeOffset Executed { get; init; }
}
