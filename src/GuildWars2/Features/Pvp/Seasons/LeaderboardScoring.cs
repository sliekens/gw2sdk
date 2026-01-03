namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a leaderboard scoring method.</summary>
[DataTransferObject]
public sealed record LeaderboardScoring
{
    /// <summary>The ID of the scoring method.</summary>
    public required string Id { get; init; }

    /// <summary>The type of the score, for example "Integer".</summary>
    public required string Type { get; init; }

    /// <summary>The description of the scoring method.</summary>
    public required string Description { get; init; }

    /// <summary>The name of the scoring method, for example "Rating", "Wins" or "losses".</summary>
    public required string Name { get; init; }

    /// <summary>The ordering of the scoring method, for example "MoreIsBetter" or "LessIsBetter".</summary>
    public required string Ordering { get; init; }
}
