namespace GuildWars2.Wvw.Matches.Stats;

[PublicAPI]
[DataTransferObject]
public sealed record MatchStats
{
    public required string Id { get; init; }

    public required Distribution Deaths { get; init; }

    public required Distribution Kills { get; init; }

    public required IReadOnlyCollection<MapSummary> Maps { get; init; }
}
