namespace GuildWars2.Wvw.Abilities;

/// <summary>Information about an ability that can be trained in World vs. World by earning and spending World Experience
/// (WXP).</summary>
[DataTransferObject]
public sealed record Ability
{
    /// <summary>The ability ID.</summary>
    public required int Id { get; init; }

    /// <summary>The ability name.</summary>
    public required string Name { get; init; }

    /// <summary>The ability description.</summary>
    public required string Description { get; init; }

    /// <summary>The URL of the ability icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the ability icon.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The ranks of the ability, with costs and effects.</summary>
    public required IReadOnlyList<AbilityRank> Ranks { get; init; }
}
