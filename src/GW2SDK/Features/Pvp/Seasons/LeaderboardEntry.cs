namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a player or team on the leaderboard for a PvP League season.</summary>
[DataTransferObject]
public sealed record LeaderboardEntry
{
    /// <summary>The account display name for solo leaderboards, or the name of the guild for guild leaderboards.</summary>
    public required string Name { get; init; }

    /// <summary>The ID of the team's guild.</summary>
    /// <remarks>This information is only recorded for guild leaderboards.</remarks>
    public required string GuildId { get; init; }

    /// <summary>The name of the team.</summary>
    /// <remarks>This information is only recorded for guild leaderboards.</remarks>
    public required string TeamName { get; init; }

    /// <summary>The ID of the team.</summary>
    /// <remarks>This information is only recorded for guild leaderboards.</remarks>
    public required int? TeamId { get; init; }

    /// <summary>The rank of the player or team on the leaderboard.</summary>
    public required int Rank { get; init; }

    /// <summary>The date and time when the player or team achieved the rank.</summary>
    public required DateTimeOffset Date { get; init; }

    /// <summary>The scores of the player or team.</summary>
    public required IReadOnlyList<Score> Scores { get; init; }
}
