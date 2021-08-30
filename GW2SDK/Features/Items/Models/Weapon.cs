using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public record Weapon : Item
    {
        public int DefaultSkin { get; init; }

        public DamageType DamageType { get; init; }

        public int MinPower { get; init; }

        public int MaxPower { get; init; }

        public int Defense { get; init; }

        public InfusionSlot[] InfusionSlots { get; init; } = Array.Empty<InfusionSlot>();

        public double AttributeAdjustment { get; init; }

        public InfixUpgrade? Prefix { get; init; }
        
        public int? SuffixItemId { get; init; }

        public int? SecondarySuffixItemId { get; init; }

        public int[]? StatChoices { get; init; }
    }
}
