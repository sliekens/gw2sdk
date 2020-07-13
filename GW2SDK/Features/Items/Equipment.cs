using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public abstract class Equipment : Item
    {
        [JsonProperty("attribute_adjustment", Required = Required.Always)]
        public double AttributeAdjustment { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [JsonProperty(Required = Required.Always)]
        public InfusionSlot[] InfusionSlots { get; set; } = new InfusionSlot[0];

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public InfixUpgrade? InfixUpgrade { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? SuffixItemId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string SecondarySuffixItemId { get; set; } = "";

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int[]? StatChoices { get; set; }
    }
}
