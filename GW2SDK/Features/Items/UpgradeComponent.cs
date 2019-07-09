using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(UpgradeComponentDiscriminatorOptions))]
    public class UpgradeComponent : Item
    {
        public int Level { get; set; }

        [NotNull]
        public UpgradeComponentFlag[] UpgradeComponentFlags { get; set; }

        [CanBeNull]
        public InfusionSlotFlag[] InfusionUpgradeFlags { get; set; }

        [NotNull]
        public InfixUpgrade InfixUpgrade { get; set; }

        [CanBeNull]
        public string[] Bonuses { get; set; }

        [CanBeNull]
        public string Suffix { get; set; }
    }
}
