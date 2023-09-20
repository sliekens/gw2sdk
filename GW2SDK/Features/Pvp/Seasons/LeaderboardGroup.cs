namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record LeaderboardGroup
{
    public required Leaderboard? Ladder { get; init; }

    public required Leaderboard? Legendary { get; init; }

    public required Leaderboard? Guild { get; init; }
}
