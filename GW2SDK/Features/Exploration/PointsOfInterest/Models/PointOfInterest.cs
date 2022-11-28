using System.Drawing;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.PointsOfInterest;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record PointOfInterest
{
    /// <summary>The name as displayed on the map, or an empty string if the PoI doesn't have a name.</summary>
    public required string Name { get; init; }

    public required int Id { get; init; }

    public required int Floor { get; init; }

    public required PointF Coordinates { get; init; }

    public required string ChatLink { get; init; }
}
