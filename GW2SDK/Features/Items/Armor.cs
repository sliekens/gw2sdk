using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public class Armor : Equipment
    {
        [JsonProperty(Required = Required.Always)]
        public string DefaultSkin { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public WeightClass WeightClass { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Defense { get; set; }
    }
}
