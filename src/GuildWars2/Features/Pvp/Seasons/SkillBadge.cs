namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a skill badge and the minimum skill rating required to obtain it.</summary>
[DataTransferObject]
public sealed record SkillBadge
{
    /// <summary>The badge name.</summary>
    public required string Name { get; init; }

    /// <summary>The badge description.</summary>
    public required string Description { get; init; }

    /// <summary>The URL of the badge icon.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The URL of the badge's large overlay image.</summary>
    public required Uri OverlayUrl { get; init; }

    /// <summary>The URL of the badge's small overlay image.</summary>
    public required Uri SmallOverlayUrl { get; init; }

    /// <summary>The badge's tiers.</summary>
    public required IImmutableValueList<SkillBadgeTier> Tiers { get; init; }
}
