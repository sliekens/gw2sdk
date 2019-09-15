using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class Bag : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool NoSellOrSort { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Size { get; set; }
    }
}
