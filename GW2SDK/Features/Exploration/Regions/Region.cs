using System.Drawing;

using GuildWars2.Exploration.Maps;

namespace GuildWars2.Exploration.Regions;

/// <summary>Information about a region like Ascalon or Kryta.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Region
{
    /// <summary>The region ID.</summary>
    public required int Id { get; init; }

    /// <summary>The region name.</summary>
    public required string Name { get; init; }

    /// <summary>The coordinates of the region label.</summary>
    public required Point LabelCoordinates { get; init; }

    /// <summary>The dimensions of the region within the continent coordinate system.</summary>
    public required Rectangle ContinentRectangle { get; init; }

    /// <summary>The maps in this region. The key is the map ID. The value is the map.</summary>
    public required Dictionary<int, Map> Maps { get; init; }
}
