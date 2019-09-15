using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Skins.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(ArmorSkinDiscriminatorOptions))]
    public class ArmorSkin : Skin
    {
        [JsonProperty(Required = Required.Always)]
        public WeightClass WeightClass { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public DyeSlotInfo DyeSlots { get; set; }
    }
}
