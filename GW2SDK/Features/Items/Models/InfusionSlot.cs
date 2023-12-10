namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfusionSlot
{
    public required InfusionSlotFlags Flags { get; init; }

    public required int? ItemId { get; init; }
}
