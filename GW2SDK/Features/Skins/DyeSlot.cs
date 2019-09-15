using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DyeSlot
    {
        [JsonProperty(Required = Required.Always)]
        public int ColorId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Material Material { get; set; }
    }
}
