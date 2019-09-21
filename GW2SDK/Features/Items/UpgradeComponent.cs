using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public class UpgradeComponent : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public UpgradeComponentFlag[] UpgradeComponentFlags { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public InfusionSlotFlag[] InfusionUpgradeFlags { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public InfixUpgrade InfixUpgrade { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string[] Bonuses { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.Always)]
        public string Suffix { get; set; }
    }
}
