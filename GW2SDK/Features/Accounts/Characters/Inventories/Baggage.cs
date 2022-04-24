using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Characters.Inventories;

[PublicAPI]
[DataTransferObject]
public sealed record Baggage
{
    public IReadOnlyCollection<Bag?> Bags { get; init; } = Array.Empty<Bag?>();
}
