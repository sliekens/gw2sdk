using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class Transmutation : Consumable
    {
        [JsonProperty(Required = Required.Always)]
        public int[] Skins { get; set; } = new int[0];
    }
}
