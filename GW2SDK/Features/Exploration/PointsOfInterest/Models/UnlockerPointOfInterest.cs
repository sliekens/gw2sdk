﻿using JetBrains.Annotations;

namespace GW2SDK.Exploration.PointsOfInterest;

[PublicAPI]
public sealed record UnlockerPointOfInterest : PointOfInterest
{
    public required string Icon { get; init; }
}
