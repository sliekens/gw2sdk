using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl;
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
        public WeightClass WeightClass { get; set; }

        public DyeSlotInfo DyeSlots { get; set; }
    }
}