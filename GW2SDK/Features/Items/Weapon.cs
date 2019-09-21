using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public class Weapon : Equipment
    {
        [JsonProperty(Required = Required.Always)]
        public int DefaultSkin { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DamageType DamageType { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MinPower { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MaxPower { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Defense { get; set; }
    }
}
