using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats;

[PublicAPI]
[DataTransferObject]
public sealed record ItemStat
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<ItemStatAttribute> Attributes { get; init; }
}
