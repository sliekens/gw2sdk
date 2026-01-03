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
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the badge icon as a Uri.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The URL of the badge's large overlay image.</summary>
    [Obsolete("Use OverlayUrl instead.")]
    public required string Overlay { get; init; }

    /// <summary>The URL of the badge's large overlay image as a Uri.</summary>
    public required Uri OverlayUrl { get; init; }

    /// <summary>The URL of the badge's small overlay image.</summary>
    [Obsolete("Use SmallOverlayUrl instead.")]
    public required string SmallOverlay { get; init; }

    /// <summary>The URL of the badge's small overlay image as a Uri.</summary>
    public required Uri SmallOverlayUrl { get; init; }

    /// <summary>The badge's tiers.</summary>
    public required IReadOnlyList<SkillBadgeTier> Tiers { get; init; }
}
