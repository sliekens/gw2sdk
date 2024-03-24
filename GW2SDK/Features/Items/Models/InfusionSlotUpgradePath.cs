namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfusionSlotUpgradePath
{
    public required InfusionSlotUpgradeKind Upgrade { get; init; }

    public required int ItemId { get; init; }
}
