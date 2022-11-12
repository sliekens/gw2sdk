using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.GodShrines;

[PublicAPI]
[DataTransferObject]
public sealed record GodShrine
{
    public required int Id { get; init; }

    public required int PointOfInterestId { get; init; }

    public required string Name { get; init; }

    public required string NameContested { get; init; }

    public required string Icon { get; init; }

    public required string IconContested { get; init; }

    public required IReadOnlyCollection<double> Coordinates { get; init; }
}
