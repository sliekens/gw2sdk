namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about how to rank players or teams on a leaderboard.</summary>
[DataTransferObject]
public sealed record LeaderboardSetting
{
    /// <summary>The name of the setting.</summary>
    public required string Name { get; init; }

    /// <summary>The ID of the primary scoring mechanism.</summary>
    public required string ScoringId { get; init; }

    /// <summary>The leaderboard tiers and details used to calculate the tier of a player or team on the leaderboard.</summary>
    public required IReadOnlyList<LeaderboardTier> Tiers { get; init; }
}
