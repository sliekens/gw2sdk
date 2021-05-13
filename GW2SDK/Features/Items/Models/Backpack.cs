using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record Backpack : Item
    {
        public int DefaultSkin { get; init; }

        public InfusionSlot[] InfusionSlots { get; init; } = new InfusionSlot[0];

        public double AttributeAdjustment { get; init; }

        public InfixUpgrade? Prefix { get; init; }
        
        public int? SuffixItemId { get; init; }

        public int[]? StatChoices { get; init; }
        
        public ItemUpgrade[]? UpgradesInto { get; init; }

        public ItemUpgrade[]? UpgradesFrom{ get; init; }
    }
}
