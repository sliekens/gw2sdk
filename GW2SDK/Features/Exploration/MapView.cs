using System.Drawing;
using JetBrains.Annotations;

namespace GW2SDK.Exploration;

[PublicAPI]
public sealed record MapView
{
    public PointF NorthWest { get; init; }

    public PointF SouthEast { get; init; }
}
