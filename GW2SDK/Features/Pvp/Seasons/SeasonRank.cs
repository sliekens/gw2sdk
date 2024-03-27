namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record SeasonRank
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    /// <summary>The URL of the season rank icon.</summary>
    public required string IconHref { get; init; }

    public required string Overlay { get; init; }

    public required string SmallOverlay { get; init; }

    public required IReadOnlyList<RankTier> Tiers { get; init; }
}
