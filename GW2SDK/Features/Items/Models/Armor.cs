using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public record Armor : Item
    {
        public int DefaultSkin { get; init; }

        public WeightClass WeightClass { get; init; }

        public int Defense { get; init; }

        public InfusionSlot[] InfusionSlots { get; init; } = new InfusionSlot[0];

        public double AttributeAdjustment { get; init; }

        public InfixUpgrade? Prefix { get; init; }
        
        public int? SuffixItemId { get; init; }

        public int[]? StatChoices { get; init; }
    }
}
