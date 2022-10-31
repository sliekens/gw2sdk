using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Adventures;

[PublicAPI]
[DataTransferObject]
public sealed record Adventure
{
    public string Id { get; init; } = "";

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public IReadOnlyCollection<double> Coordinates { get; init; } = Array.Empty<double>();
}
