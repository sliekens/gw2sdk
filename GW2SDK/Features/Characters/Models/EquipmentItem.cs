using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record EquipmentItem
    {
        /// <summary>The item ID.</summary>
        public int Id { get; init; }

        /// <summary>The number of this item in the armory (i.e. not currently active in any slot).</summary>
        public int? Count { get; init; }

        /// <summary>The slot where this item is equipped   .</summary>
        public EquipmentSlot? Slot { get; init; }

        /// <summary>The item IDs of runes or sigils in this item.</summary>
        public int[]? Upgrades { get; init; }

        /// <summary>The item IDs of infusions in this item.</summary>
        public int[]? Infusions { get; init; }

        /// <summary>The skin ID.</summary>
        public int? SkinId { get; init; }

        /// <summary>The attribute combination for items with selectable stats.</summary>
        public SelectedStat? Stats { get; init; } = new();

        /// <summary>Whether this item is bound.</summary>
        public ItemBinding Binding { get; init; }

        /// <summary>The name of the character if the item is soulbound.</summary>
        public string BoundTo { get; init; } = "";

        /// <summary>Whether this item is currently equipped or stored in the (legendary) armory.</summary>
        public EquipmentLocation Location { get; init; }

        /// <summary>The equipment tab numbers where this item is (re)used.</summary>
        public int[]? Tabs { get; init; } = Array.Empty<int>();

        // Always length 4
        /// <summary>The IDs of colors applied to the current item.</summary>
        public int?[]? Dyes { get; init; }
    }
}
