using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Maps.Models;

[PublicAPI]
[DataTransferObject]
public sealed record WorldRegion
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public PointF LabelCoordinates { get; init; }

    public ContinentRectangle ContinentRectangle { get; init; } = new();

    public Dictionary<int, Map> Maps { get; init; } = new(0);
}
