namespace GuildWars2.Wvw.Matches;

/// <summary>Information about a skirmish.</summary>
[DataTransferObject]
public sealed record Skirmish
{
    /// <summary>The skirmish ID.</summary>
    public required int Id { get; init; }

    /// <summary>The scores of each team in the skirmish.</summary>
    public required Distribution Scores { get; init; }

    /// <summary>The scores of each team in the skirmish, grouped by map.</summary>
    public required IReadOnlyCollection<MapScores> MapScores { get; init; }
}
