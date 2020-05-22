using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DyeSlotInfo
    {
        [JsonProperty(Required = Required.Always)]
        public DyeSlot?[] Default { get; set; } = new DyeSlot?[0];

        [JsonProperty(Required = Required.Always)]
        public DyeSlotOverrideInfo Overrides { get; set; } = new DyeSlotOverrideInfo();
    }
}
