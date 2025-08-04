namespace GuildWars2.Wvw.Upgrades;

/// <summary>Information about a tier of an objective upgrade.</summary>
[DataTransferObject]
public sealed record UpgradeTier
{
    /// <summary>The name of the tier.</summary>
    public required string Name { get; init; }

    /// <summary>The amount of yaks which must reach the objective to unlock this tier.</summary>
    public required int YaksRequired { get; init; }

    /// <summary>The upgrade effects provided by this tier.</summary>
    public required IReadOnlyList<Upgrade> Upgrades { get; init; }
}
