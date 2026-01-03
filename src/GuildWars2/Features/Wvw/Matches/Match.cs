namespace GuildWars2.Wvw.Matches;

/// <summary>Information about a match in the World versus World (WvW) game mode.</summary>
[DataTransferObject]
public sealed record Match
{
    /// <summary>The match ID.</summary>
    public required string Id { get; init; }

    /// <summary>The worlds participating in the match.</summary>
    public required Worlds Worlds { get; init; }

    /// <summary>All worlds in the match.</summary>
    public required AllWorlds AllWorlds { get; init; }

    /// <summary>The start time of the match.</summary>
    public required DateTimeOffset StartTime { get; init; }

    /// <summary>The end time of the match.</summary>
    public required DateTimeOffset EndTime { get; init; }

    /// <summary>The scores distribution of the teams in the match.</summary>
    public required Distribution Scores { get; init; }

    /// <summary>The kills distribution of the teams in the match.</summary>
    public required Distribution Kills { get; init; }

    /// <summary>The deaths distribution of the teams in the match.</summary>
    public required Distribution Deaths { get; init; }

    /// <summary>The victory points distribution of the teams in the match.</summary>
    public required Distribution VictoryPoints { get; init; }

    /// <summary>The skirmishes in the match.</summary>
    public required IReadOnlyCollection<Skirmish> Skirmishes { get; init; }

    /// <summary>The maps in the match.</summary>
    public required IReadOnlyCollection<Map> Maps { get; init; }
}
