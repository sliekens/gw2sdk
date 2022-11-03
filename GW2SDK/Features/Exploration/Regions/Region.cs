using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using GW2SDK.Exploration.Maps;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Regions;

[PublicAPI]
[DataTransferObject]
public sealed record Region
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public PointF LabelCoordinates { get; init; }

    public Area ContinentRectangle { get; init; } = new();

    public Dictionary<int, Map> Maps { get; init; } = new(0);
}
