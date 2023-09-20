using System.Drawing;

namespace GuildWars2.Exploration;

/// <summary>The dimensions of a map, given as the coordinates of the lower-left (SW) and upper-right (NE) corners.</summary>
[PublicAPI]
public sealed record MapArea
{
    public required PointF SouthWest { get; init; }

    public required PointF NorthEast { get; init; }
}
