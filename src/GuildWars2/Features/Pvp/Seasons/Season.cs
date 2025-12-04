namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a PvP season.</summary>
[DataTransferObject]
public sealed record Season
{
    /// <summary>The season ID.</summary>
    public required string Id { get; init; }

    /// <summary>The season name.</summary>
    public required string Name { get; init; }

    /// <summary>The season start date.</summary>
    public required DateTime Start { get; init; }

    /// <summary>The season end date.</summary>
    public required DateTime End { get; init; }

    /// <summary>Indicates whether it is the active season.</summary>
    public required bool Active { get; init; }

    /// <summary>The season's divisions, which is a pips-based system to measure progress within the season.</summary>
    /// <remarks>In the first 4 seasons, pips were used as a measure of skill, and it was possible to lose pips. Divisions also
    /// played a role in matchmaking, only players in the same division could be matched together. Pips-based matchmaking was
    /// replaced by a skill rating system in season five (details in <see cref="Ranks" />), and pips are now a purely
    /// reward-based system.</remarks>
    public required IReadOnlyList<Division> Divisions { get; init; }

    /// <summary>The skill ranks and skill rating requirements for each rank.</summary>
    /// <remarks>This skill rating system was introduced in season five, it is <c>null</c> for earlier seasons. The first 4
    /// seasons used a pips-based system (see <see cref="Divisions" />) instead of skill rating.</remarks>
    public required IReadOnlyList<SkillBadge>? Ranks { get; init; }

    /// <summary>The season's leaderboards.</summary>
    public required LeaderboardGroup Leaderboards { get; init; }
}
