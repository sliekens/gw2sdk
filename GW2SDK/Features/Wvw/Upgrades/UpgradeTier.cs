namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
[DataTransferObject]
public sealed record UpgradeTier
{
    public required string Name { get; init; }

    public required int YaksRequired { get; init; }

    public required IReadOnlyList<Upgrade> Upgrades { get; init; }
}
