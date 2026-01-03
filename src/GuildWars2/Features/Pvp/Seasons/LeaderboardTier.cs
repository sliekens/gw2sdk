namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a leaderboard tier.</summary>
[DataTransferObject]
public sealed record LeaderboardTier
{
    /// <summary>The color of the tier.</summary>
    public required string Color { get; init; }

    /// <summary>The method used to calculate the tier of a player or team on the leaderboard.</summary>
    public required Extensible<LeaderboardTierKind>? Kind { get; init; }

    /// <summary>The name of the tier.</summary>
    public required string Name { get; init; }

    /// <summary>The minimum and maximum rank for this tier.</summary>
    public required LeaderboardTierRange Range { get; init; }
}
