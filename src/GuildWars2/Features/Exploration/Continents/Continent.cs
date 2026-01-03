using System.Drawing;

namespace GuildWars2.Exploration.Continents;

/// <summary>Information about a continent like Tyria or the Mists.</summary>
[DataTransferObject]
public sealed record Continent
{
    /// <summary>The id of the continent.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the continent.</summary>
    public required string Name { get; init; }

    /// <summary>The width and height dimensions of the continent.</summary>
    public required Size ContinentDimensions { get; init; }

    /// <summary>The minimal zoom level for use with the map tile service.</summary>
    public required int MinZoom { get; init; }

    /// <summary>The maximum zoom level for use with the map tile service.</summary>
    public required int MaxZoom { get; init; }

    /// <summary>A list of floors ids available for this continent.</summary>
    public required IReadOnlyList<int> Floors { get; init; }
}
