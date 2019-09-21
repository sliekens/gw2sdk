using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [Inheritable]
    public class ArmorSkin : Skin
    {
        [JsonProperty(Required = Required.Always)]
        public WeightClass WeightClass { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DyeSlotInfo DyeSlots { get; set; }
    }
}
