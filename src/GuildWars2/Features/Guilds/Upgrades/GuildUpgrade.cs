namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a guild upgrade. This class is the base type for all guild upgrades. Cast objects of this
/// type to a more specific type to access more properties.</summary>
[Inheritable]
[DataTransferObject]
public record GuildUpgrade
{
    /// <summary>The guild upgrade ID.</summary>
    public required int Id { get; init; }

    /// <summary>The guild upgrade name.</summary>
    public required string Name { get; init; }

    /// <summary>The guild upgrade description.</summary>
    public required string Description { get; init; }

    /// <summary>The processing time of the guild upgrade.</summary>
    public required TimeSpan BuildTime { get; init; }

    /// <summary>The URL of the guild upgrade icon.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The minimum guild level require to unlock the guild upgrade.</summary>
    public required int RequiredLevel { get; init; }

    /// <summary>The amount of guild XP awarded.</summary>
    public required int Experience { get; init; }

    /// <summary>The IDs of guild upgrades that must be completed first.</summary>
    public required IReadOnlyList<int> Prerequisites { get; init; }

    /// <summary>The costs of the guild upgrade.</summary>
    public required IReadOnlyList<GuildUpgradeCost> Costs { get; init; }
}
