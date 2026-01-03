namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about the scores of a player or team on a leaderboard.</summary>
[DataTransferObject]
public sealed record Score
{
    /// <summary>The scoring mechanism ID, for cross-refencing with the leaderboard's scoring settings.</summary>
    public required string Id { get; init; }

    /// <summary>The actual score.</summary>
    public required int Value { get; init; }
}
