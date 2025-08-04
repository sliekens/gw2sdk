namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about the minimum and maximum rank of a leaderboard tier.</summary>
[DataTransferObject]
public sealed record LeaderboardTierRange
{
    /// <summary>The minimum rank of the tier.</summary>
    public required double Minimum { get; init; }

    /// <summary>The maximum rank of the tier.</summary>
    public required double Maximum { get; init; }
}
