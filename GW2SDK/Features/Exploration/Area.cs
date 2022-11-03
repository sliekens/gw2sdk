using System.Drawing;
using JetBrains.Annotations;

namespace GW2SDK.Exploration;

/// <summary>The dimensions of a map within the continent coordinate system, given as top-left (NW) and bottom-right (SE)
/// corner coordinates.</summary>
[PublicAPI]
public sealed record Area
{
    public PointF NorthWest { get; init; }

    public PointF SouthEast { get; init; }
}
