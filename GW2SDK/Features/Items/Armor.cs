using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(ArmorDiscriminatorOptions))]
    public class Armor : Equipment
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string DefaultSkin { get; set; }

        [JsonProperty(Required = Required.Always)]
        public WeightClass WeightClass { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Defense { get; set; }
    }
}
