namespace GuildWars2.Commerce.Delivery;

[PublicAPI]
[DataTransferObject]
public sealed record DeliveredItem
{
    public required int Id { get; init; }

    public required int Count { get; init; }
}
