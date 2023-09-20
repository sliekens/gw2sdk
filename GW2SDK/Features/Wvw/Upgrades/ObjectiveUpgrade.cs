namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
[DataTransferObject]
public sealed record ObjectiveUpgrade
{
    public required int Id { get; init; }

    public required IReadOnlyCollection<UpgradeTier> Tiers { get; init; }
}
