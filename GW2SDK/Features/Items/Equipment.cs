using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public abstract class Equipment : Item
    {
        public int Level { get; set; }

        [NotNull]
        public InfusionSlot[] InfusionSlots { get; set; }

        [NotNull]
        public InfixUpgrade InfixUpgrade { get; set; }

        public int? SuffixItemId { get; set; }

        [NotNull]
        public string SecondarySuffixItemId { get; set; }

        [CanBeNull]
        public int[] StatChoices { get; set; }
    }
}
