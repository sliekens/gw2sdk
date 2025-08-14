namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a leaderboard for a PvP season.</summary>
[DataTransferObject]
public sealed record Leaderboard
{
    /// <summary>Various settings for how to rank players or teams on the leaderboard.</summary>
    public required LeaderboardSetting Settings { get; init; }

    /// <summary>The scoring methods, used for cross-referencing with leaderboard entries.</summary>
    public required IReadOnlyList<LeaderboardScoring> Scorings { get; init; }
}
