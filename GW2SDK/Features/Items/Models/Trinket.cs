using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public record Trinket : Item
    {
        public InfusionSlot[] InfusionSlots { get; init; } = Array.Empty<InfusionSlot>();

        public double AttributeAdjustment { get; init; }

        public InfixUpgrade? Prefix { get; init; }

        public int? SuffixItemId { get; init; }
        
        public int[]? StatChoices { get; init; }

        public ItemUpgrade[]? UpgradesInto { get; init; }

        public ItemUpgrade[]? UpgradesFrom { get; init; }
    }
}
