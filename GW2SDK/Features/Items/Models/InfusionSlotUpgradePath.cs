namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfusionSlotUpgradePath
{
    public required UpgradeType Upgrade { get; init; }

    public required int ItemId { get; init; }
}
