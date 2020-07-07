using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DistanceTraitFact : TraitFact
    {
        [JsonProperty("distance", Required = Required.Always)]
        public int Distance { get; set; }
    }
}