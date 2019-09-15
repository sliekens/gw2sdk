using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class Transmutation : Consumable
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public int[] Skins { get; set; }
    }
}
