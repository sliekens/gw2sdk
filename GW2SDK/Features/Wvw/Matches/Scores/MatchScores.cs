namespace GuildWars2.Wvw.Matches.Scores;

/// <summary>Information about a match in the World versus World (WvW) game mode.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record MatchScores
{
    /// <summary>The match ID.</summary>
    public required string Id { get; init; }

    /// <summary>The scores distribution of the teams in the match.</summary>
    public required Distribution Scores { get; init; }

    /// <summary>The victory points distribution of the teams in the match.</summary>
    public required Distribution VictoryPoints { get; init; }

    /// <summary>The skirmishes in the match.</summary>
    public required IReadOnlyCollection<Skirmish> Skirmishes { get; init; }

    /// <summary>The maps in the match.</summary>
    public required IReadOnlyCollection<MapSummary> Maps { get; init; }
}
