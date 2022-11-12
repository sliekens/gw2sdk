using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using GW2SDK.Exploration.Charts;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Regions;

[PublicAPI]
[DataTransferObject]
public sealed record Region
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required PointF LabelCoordinates { get; init; }

    public required Area ContinentRectangle { get; init; }

    public required Dictionary<int, Chart> Maps { get; init; }
}
