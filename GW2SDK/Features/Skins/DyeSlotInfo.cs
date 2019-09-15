using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DyeSlotInfo
    {
        [NotNull]
        [ItemCanBeNull]
        [JsonProperty(Required = Required.Always)]
        public DyeSlot[] Default { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public DyeSlotOverrideInfo Overrides { get; set; }
    }
}
