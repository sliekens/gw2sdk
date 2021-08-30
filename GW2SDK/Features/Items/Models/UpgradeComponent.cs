using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public record UpgradeComponent : Item
    {
        public UpgradeComponentFlag[] UpgradeComponentFlags { get; init; } = Array.Empty<UpgradeComponentFlag>();

        public InfusionSlotFlag[] InfusionUpgradeFlags { get; init; } = Array.Empty<InfusionSlotFlag>();

        public double AttributeAdjustment { get; init; }

        public InfixUpgrade Suffix { get; init; } = new();

        public string SuffixName { get; init; } = "";
    }
}
