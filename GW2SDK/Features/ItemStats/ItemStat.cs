using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record ItemStat
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public ItemStatAttribute[] Attributes { get; init; } = Array.Empty<ItemStatAttribute>();
    }
}
