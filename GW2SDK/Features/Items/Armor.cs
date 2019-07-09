using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl;
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
        public string DefaultSkin { get; set; }

        public WeightClass WeightClass { get; set; }
        
        public int Defense { get; set; }
    }
}