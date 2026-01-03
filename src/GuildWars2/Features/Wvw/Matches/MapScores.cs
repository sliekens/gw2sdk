using GuildWars2.Exploration.Maps;

namespace GuildWars2.Wvw.Matches;

/// <summary>Information about the scores of a map in a World vs. World match.</summary>
[DataTransferObject]
public sealed record MapScores
{
    /// <summary>The map kind.</summary>
    public required Extensible<MapKind> Kind { get; init; }

    /// <summary>The scores of the teams on this map.</summary>
    public required Distribution Scores { get; init; }
}
