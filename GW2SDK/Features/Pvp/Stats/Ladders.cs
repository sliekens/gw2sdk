namespace GuildWars2.Pvp.Stats;

/// <summary>Information about the statistics of a player in various PvP ladders.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Ladders
{
    /// <summary>Statistics not associated with any ladder.</summary>
    public required Results? None { get; init; }

    /// <summary>Statistics for unranked play.</summary>
    public required Results? Unranked { get; init; }

    /// <summary>Statistics for ranked games (5v5).</summary>
    public required Results? Ranked { get; init; }

    /// <summary>Statistics for ranked games (2v2).</summary>
    public required Results? Ranked2v2 { get; init; }

    /// <summary>Statistics for ranked games (3v3).</summary>
    public required Results? Ranked3v3 { get; init; }

    /// <summary>Statistics for ranked games (Push).</summary>
    public required Results? PushRanked { get; init; }

    /// <summary>Statistics for custom solo arenas.</summary>
    public required Results? SoloArenaRated { get; init; }

    /// <summary>Statistics for custom team arenas.</summary>
    public required Results? TeamArenaRated { get; init; }
}
