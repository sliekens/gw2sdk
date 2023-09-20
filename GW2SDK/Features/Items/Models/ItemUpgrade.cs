namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record ItemUpgrade
{
    public required UpgradeType Upgrade { get; init; }

    public required int ItemId { get; init; }
}
