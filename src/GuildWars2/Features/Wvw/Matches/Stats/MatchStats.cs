namespace GuildWars2.Wvw.Matches.Stats;

/// <summary>Information about a match in the World versus World (WvW) game mode.</summary>
[DataTransferObject]
public sealed record MatchStats
{
    /// <summary>The match ID.</summary>
    public required string Id { get; init; }

    /// <summary>The kills distribution of the teams in the match.</summary>
    public required Distribution Kills { get; init; }

    /// <summary>The deaths distribution of the teams in the match.</summary>
    public required Distribution Deaths { get; init; }

    /// <summary>The maps in the match.</summary>
    public required IImmutableValueList<MapSummary> Maps { get; init; }
}
