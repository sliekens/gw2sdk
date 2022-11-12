using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Inventories;

[PublicAPI]
[DataTransferObject]
public sealed record Baggage
{
    public required IReadOnlyCollection<Bag?> Bags { get; init; }
}
