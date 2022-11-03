using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Maps;

[PublicAPI]
[DataTransferObject]
public sealed record Map
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public int MinLevel { get; init; }

    public int MaxLevel { get; init; }

    public int DefaultFloor { get; init; }

    public MapKind Kind { get; init; }

    public IReadOnlyCollection<int> Floors { get; init; } = Array.Empty<int>();

    public int? RegionId { get; init; }

    public string RegionName { get; init; } = "";

    public int? ContinentId { get; init; }

    public string ContinentName { get; init; } = "";

    public MapArea MapRectangle { get; init; } = new();

    public Area ContinentRectangle { get; init; } = new();
}
