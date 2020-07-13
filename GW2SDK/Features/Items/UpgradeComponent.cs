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

        [JsonProperty(Required = Required.Always)]
        public UpgradeComponentFlag[] UpgradeComponentFlags { get; set; } = new UpgradeComponentFlag[0];

        [JsonProperty(Required = Required.Always)]
        public InfusionSlotFlag[] InfusionUpgradeFlags { get; set; } = new InfusionSlotFlag[0];

        [JsonProperty("attribute_adjustment", Required = Required.Always)]
        public double AttributeAdjustment { get; set; }

        [JsonProperty(Required = Required.Always)]
        public InfixUpgrade InfixUpgrade { get; set; } = new InfixUpgrade();

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string[]? Bonuses { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string? Suffix { get; set; }
    }
}
