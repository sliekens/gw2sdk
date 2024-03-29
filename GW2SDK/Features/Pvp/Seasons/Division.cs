namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a division in a PvP season.</summary>
/// <remarks>In the first 4 seasons, division was an indication of player skill and used in the matchmaking algorithm. Only
/// players within the same division could be matched together. As of season five, it is a purely reward-based system and
/// it no longer plays a role in matchmaking.</remarks>
[PublicAPI]
[DataTransferObject]
public sealed record Division
{
    /// <summary>The division name.</summary>
    public required string Name { get; init; }

    /// <summary>Modifiers for the division.</summary>
    public required DivisionFlags Flags { get; init; }

    /// <summary>The URL of the large division icon.</summary>
    public required string LargeIconHref { get; init; }

    /// <summary>The URL of the small division icon.</summary>
    public required string SmallIconHref { get; init; }

    /// <summary>The URL of the pip icon.</summary>
    public required string PipIconHref { get; init; }

    /// <summary>The division's tiers.</summary>
    public required IReadOnlyList<DivisionTier> Tiers { get; init; }
}
