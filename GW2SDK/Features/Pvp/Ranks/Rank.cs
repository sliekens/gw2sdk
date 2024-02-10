namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
[DataTransferObject]
public sealed record Rank
{
    public required int Id { get; init; }

    public required int FinisherId { get; init; }

    public required string Name { get; init; }

    /// <summary>The URL of the rank icon.</summary>
    public required string IconHref { get; init; }

    public required int MinRank { get; init; }

    public required int MaxRank { get; init; }

    public required IReadOnlyCollection<Level> Levels { get; init; }
}
