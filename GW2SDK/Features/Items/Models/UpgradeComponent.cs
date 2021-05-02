using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public record UpgradeComponent : Item
    {
        public UpgradeComponentFlag[] UpgradeComponentFlags { get; init; } = new UpgradeComponentFlag[0];

        public InfusionSlotFlag[] InfusionUpgradeFlags { get; init; } = new InfusionSlotFlag[0];

        public double AttributeAdjustment { get; init; }

        public InfixUpgrade Suffix { get; init; } = new();

        public string SuffixName { get; init; } = "";
    }
}
