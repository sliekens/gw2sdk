namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a skill badge and the minimum skill rating required to obtain it.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SkillBadge
{
    /// <summary>The badge name.</summary>
    public required string Name { get; init; }

    /// <summary>The badge description.</summary>
    public required string Description { get; init; }

    /// <summary>The URL of the badge icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The URL of the badge's large overlay image.</summary>
    public required string Overlay { get; init; }

    /// The URL of the badge's small overlay image.
    public required string SmallOverlay { get; init; }

    /// <summary>The badge's tiers.</summary>
    public required IReadOnlyList<SkillBadgeTier> Tiers { get; init; }
}
