namespace GuildWars2.Commerce.Delivery;

/// <summary>An item in the delivery box.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record DeliveredItem
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>The number of items in the delivery box.</summary>
    public required int Count { get; init; }
}
