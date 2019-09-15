using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public abstract class Equipment : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public InfusionSlot[] InfusionSlots { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public InfixUpgrade InfixUpgrade { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int? SuffixItemId { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string SecondarySuffixItemId { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public int[] StatChoices { get; set; }
    }
}
