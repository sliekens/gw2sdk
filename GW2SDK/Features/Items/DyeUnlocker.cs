using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class DyeUnlocker : Unlocker
    {
        [JsonProperty(Required = Required.Always)]
        public int ColorId { get; set; }
    }
}
