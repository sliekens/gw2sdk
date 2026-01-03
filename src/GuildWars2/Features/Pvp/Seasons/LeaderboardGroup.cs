namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about the leaderboards for a PvP League season.</summary>
[DataTransferObject]
public sealed record LeaderboardGroup
{
    /// <summary>A solo leaderboard, added in season five and later.</summary>
    public required Leaderboard? Ladder { get; init; }

    /// <summary>A solo leaderboard, only used in the first four seasons.</summary>
    public required Leaderboard? Legendary { get; init; }

    /// <summary>A team leaderboard, only used in the first four seasons.</summary>
    public required Leaderboard? Guild { get; init; }
}
