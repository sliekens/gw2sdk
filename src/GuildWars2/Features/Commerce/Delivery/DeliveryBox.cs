namespace GuildWars2.Commerce.Delivery;

/// <summary>Information about items and coins ready for pickup from the Black Lion Trading Company.</summary>
[DataTransferObject]
public sealed record DeliveryBox
{
    /// <summary>The number of coins in the delivery box.</summary>
    public required Coin Coins { get; init; }

    /// <summary>The items in the delivery box.</summary>
    public required IImmutableValueList<DeliveredItem> Items { get; init; }
}
