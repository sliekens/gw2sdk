using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class PercentTraitFact : TraitFact
    {
        [JsonProperty("percent", Required = Required.Always)]
        public new int Percent { get; set; }
    }
}