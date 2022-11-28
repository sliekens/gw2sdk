using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.ItemStats;

[PublicAPI]
[DataTransferObject]
public sealed record ItemStat
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<ItemStatAttribute> Attributes { get; init; }
}
