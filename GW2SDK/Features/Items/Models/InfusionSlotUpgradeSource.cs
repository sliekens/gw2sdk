namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfusionSlotUpgradeSource
{
    public required InfusionSlotUpgradeKind Upgrade { get; init; }

    public required int ItemId { get; init; }
}
