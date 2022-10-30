using System.Drawing;
using JetBrains.Annotations;

namespace GW2SDK.Exploration;

[PublicAPI]
public sealed record MapView2
{
    public PointF SouthWest { get; init; }

    public PointF NorthEast { get; init; }
}
