namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
[DataTransferObject]
public sealed record GuildUpgradeCost
{
    public required GuildUpgradeCostKind Kind { get; init; }

    public required string Name { get; init; }

    public required int Count { get; init; }

    public required int? ItemId { get; init; }
}
