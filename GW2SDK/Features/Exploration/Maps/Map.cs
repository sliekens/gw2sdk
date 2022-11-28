using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.Maps;

[PublicAPI]
[DataTransferObject]
public sealed record Map
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required int MinLevel { get; init; }

    public required int MaxLevel { get; init; }

    public required int DefaultFloor { get; init; }

    public required MapKind Kind { get; init; }

    public required IReadOnlyCollection<int> Floors { get; init; }

    public required int? RegionId { get; init; }

    public required string RegionName { get; init; }

    public required int? ContinentId { get; init; }

    public required string ContinentName { get; init; }

    public required MapArea MapRectangle { get; init; }

    public required Area ContinentRectangle { get; init; }
}
