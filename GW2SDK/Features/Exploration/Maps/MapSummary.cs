using System.Drawing;

namespace GuildWars2.Exploration.Maps;

/// <summary>Information about a map.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record MapSummary
{
    /// <summary>The map ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the map.</summary>
    public required string Name { get; init; }

    /// <summary>The minimum player level of the map.</summary>
    public required int MinLevel { get; init; }

    /// <summary>The maximum player level of the map.</summary>
    public required int MaxLevel { get; init; }

    /// <summary>The default floor of the map.</summary>
    public required int DefaultFloor { get; init; }

    /// <summary>The kind of map.</summary>
    public required MapKind Kind { get; init; }

    /// <summary>The floors of the map.</summary>
    public required IReadOnlyCollection<int> Floors { get; init; }

    /// <summary>The region ID, or null for certain instance maps.</summary>
    public required int? RegionId { get; init; }

    /// <summary>The name of the region, or empty for certain instance maps.</summary>
    public required string RegionName { get; init; }

    /// <summary>The continent ID, or null for certain instance maps.</summary>
    public required int? ContinentId { get; init; }

    /// <summary>The name of the continent, or empty for certain instance maps.</summary>
    public required string ContinentName { get; init; }

    /// <summary>The dimensions of the map.</summary>
    public required Rectangle MapRectangle { get; init; }

    /// <summary>The dimensions of the map within the continent coordinate system.</summary>
    public required Rectangle ContinentRectangle { get; init; }
}
