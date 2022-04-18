using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats.Models;

[PublicAPI]
[DataTransferObject]
public sealed record ItemStat
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public IReadOnlyCollection<ItemStatAttribute> Attributes { get; init; } =
        Array.Empty<ItemStatAttribute>();
}
