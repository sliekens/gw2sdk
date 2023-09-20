namespace GuildWars2.Wvw.Matches.Scores;

[PublicAPI]
[DataTransferObject]
public sealed record MapSummary
{
    public required int Id { get; init; }

    public required MapKind Kind { get; init; }

    public required Distribution Scores { get; init; }
}
