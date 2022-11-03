﻿using System.Drawing;
using JetBrains.Annotations;

namespace GW2SDK.Exploration;

/// <summary>The dimensions of a map, given as the coordinates of the lower-left (SW) and upper-right (NE) corners.</summary>
[PublicAPI]
public sealed record MapArea
{
    public PointF SouthWest { get; init; }

    public PointF NorthEast { get; init; }
}