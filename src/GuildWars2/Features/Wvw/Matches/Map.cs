using GuildWars2.Exploration.Maps;

namespace GuildWars2.Wvw.Matches;

/// <summary>Information about a map in a World vs. World match.</summary>
[DataTransferObject]
public sealed record Map
{
    /// <summary>The map ID.</summary>
    public required int Id { get; init; }

    /// <summary>The map kind.</summary>
    public required Extensible<MapKind> Kind { get; init; }

    /// <summary>The scores of the teams on this map.</summary>
    public required Distribution Scores { get; init; }

    /// <summary>The bonuses in this map and who owns them.</summary>
    public required IReadOnlyCollection<Bonus> Bonuses { get; init; }

    /// <summary>The objectives in this map and who claimed them.</summary>
    public required IReadOnlyCollection<OwnedObjective> Objectives { get; init; }

    /// <summary>The deaths distribution of the teams on this map.</summary>
    public required Distribution Deaths { get; init; }

    /// <summary>The kills distribution of the teams on this map.</summary>
    public required Distribution Kills { get; init; }
}
