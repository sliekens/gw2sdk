using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Inventories;

[PublicAPI]
[DataTransferObject]
public sealed record Baggage
{
    public required IReadOnlyCollection<Bag?> Bags { get; init; }
}
