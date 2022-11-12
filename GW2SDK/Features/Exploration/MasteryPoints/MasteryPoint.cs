using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.MasteryPoints;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryPoint
{
    public required int Id { get; init; }

    public required MasteryRegionName Region { get; init; }

    public required IReadOnlyCollection<double> Coordinates { get; init; }
}
