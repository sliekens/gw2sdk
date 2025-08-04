namespace GuildWars2.Wvw.Upgrades;

/// <summary>Information about a World vs. World objective upgrade.</summary>
[DataTransferObject]
public sealed record ObjectiveUpgrade
{
    /// <summary>The upgrade ID.</summary>
    public required int Id { get; init; }

    /// <summary>The upgrade tiers.</summary>
    public required IReadOnlyList<UpgradeTier> Tiers { get; init; }
}
