using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.PointsOfInterest;

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
