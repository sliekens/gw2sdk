namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record LeaderboardSetting
{
    public required string Name { get; init; }

    public required string ScoringId { get; init; }

    public required IReadOnlyList<LeaderboardTier> Tiers { get; init; }
}
