namespace GuildWars2.Wvw.Matches.Stats;

[PublicAPI]
[DataTransferObject]
public sealed record MapSummary
{
    public required int Id { get; init; }

    public required MapKind Kind { get; init; }

    public required Distribution Deaths { get; init; }

    public required Distribution Kills { get; init; }
}
