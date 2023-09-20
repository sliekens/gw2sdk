namespace GuildWars2.Wvw.Ranks;

[PublicAPI]
[DataTransferObject]
public sealed record Rank
{
    public required int Id { get; init; }

    public required string Title { get; init; }

    public required int MinRank { get; init; }
}
