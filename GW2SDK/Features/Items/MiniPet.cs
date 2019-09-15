using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class Minipet : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MinipetId { get; set; }
    }
}
