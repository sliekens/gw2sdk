using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Skins;
using Newtonsoft.Json;

namespace GW2SDK.Features.Skins
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