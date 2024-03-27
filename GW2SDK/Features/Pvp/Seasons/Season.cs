namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record Season
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required DateTime Start { get; init; }

    public required DateTime End { get; init; }

    public required bool Active { get; init; }

    public required IReadOnlyList<Division> Divisions { get; init; }

    public required IReadOnlyList<SeasonRank>? Ranks { get; init; }

    public required LeaderboardGroup Leaderboards { get; init; }
}
