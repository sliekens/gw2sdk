using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class CraftingMaterial : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }
    }
}
