using System;
using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Hearts;

[PublicAPI]
[DataTransferObject]
public sealed record Heart
{
    public int Id { get; init; }

    public string Objective { get; init; } = "";

    public int Level { get; init; }

    public PointF Coordinates { get; init; }

    public IReadOnlyCollection<PointF> Boundaries { get; init; } = Array.Empty<PointF>();

    public string ChatLink { get; init; } = "";
}
