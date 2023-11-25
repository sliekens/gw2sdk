namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record GuildUpgrade
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required TimeSpan BuildTime { get; init; }

    public required string IconHref { get; init; }

    public required int RequiredLevel { get; init; }

    public required int Experience { get; init; }

    public required IReadOnlyCollection<int> Prerequisites { get; init; }

    public required IReadOnlyCollection<GuildUpgradeCost> Costs { get; init; }
}
