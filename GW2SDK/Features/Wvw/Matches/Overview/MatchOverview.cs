namespace GuildWars2.Wvw.Matches.Overview;

/// <summary>Information about a match in the World versus World (WvW) game mode.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record MatchOverview
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
}
