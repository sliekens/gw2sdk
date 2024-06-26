﻿using GuildWars2.Exploration.Maps;

namespace GuildWars2.Wvw.Matches.Scores;

/// <summary>Information about a map in a World vs. World match.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record MapSummary
{
    /// <summary>The map ID.</summary>
    public required int Id { get; init; }

    /// <summary>The map kind.</summary>
    public required Extensible<MapKind> Kind { get; init; }

    /// <summary>The scores of the teams on this map.</summary>
    public required Distribution Scores { get; init; }
}
