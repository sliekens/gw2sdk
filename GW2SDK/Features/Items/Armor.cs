using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Items;
using Newtonsoft.Json;

namespace GW2SDK.Features.Items
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