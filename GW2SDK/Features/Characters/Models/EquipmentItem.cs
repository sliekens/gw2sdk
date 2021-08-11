using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record EquipmentItem
    {
        public int Id { get; init; }

        public int? Count { get; init; }

        public EquipmentSlot? Slot { get; init; }

        public int[]? Upgrades { get; init; }

        public int[]? Infusions { get; init; }

        public int? SkinId { get; init; }

        public SelectedStat? Stats { get; init; } = new();

        public ItemBinding Binding { get; init; }

        public string BoundTo { get; init; } = "";

        public EquipmentLocation Location { get; init; }

        public int[]? Tabs { get; init; } = Array.Empty<int>();

        // Always length 4
        public int?[]? Dyes { get; init; }
    }
}