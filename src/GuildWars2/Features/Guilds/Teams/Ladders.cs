namespace GuildWars2.Guilds.Teams;

/// <summary>Information about a guild's wins and losses per game mode.</summary>
[DataTransferObject]
public sealed record Ladders
{
    /// <summary>The team's results that don't fit in any other game mode.</summary>
    public required Results? None { get; init; }

    /// <summary>The team's results in Unranked PvP.</summary>
    public required Results? Unranked { get; init; }

    /// <summary>The team's results in Ranked PvP.</summary>
    public required Results? Ranked { get; init; }

    /// <summary>The team's results in 2v2 Ranked PvP.</summary>
    public required Results? Ranked2v2 { get; init; }

    /// <summary>The team's results in 3v3 Ranked PvP.</summary>
    public required Results? Ranked3v3 { get; init; }

    /// <summary>The team's results in Custom Arenas.</summary>
    public required Results? SoloArenaRated { get; init; }

    /// <summary>The team's results in Custom Arenas.</summary>
    public required Results? TeamArenaRated { get; init; }
}
