namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[DataTransferObject]
public sealed record Match
{
    public required string Id { get; init; }

    public required Worlds Worlds { get; init; }

    public required AllWorlds AllWorlds { get; init; }

    public required DateTimeOffset StartTime { get; init; }

    public required DateTimeOffset EndTime { get; init; }

    public required Distribution Scores { get; init; }

    public required Distribution Kills { get; init; }

    public required Distribution Deaths { get; init; }

    public required Distribution VictoryPoints { get; init; }

    public required IReadOnlyCollection<Skirmish> Skirmishes { get; init; }

    public required IReadOnlyCollection<Map> Maps { get; init; }
}
