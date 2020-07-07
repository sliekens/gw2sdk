using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class BuffConversionTraitFact : TraitFact
    {
        [JsonProperty("percent", Required = Required.Always)]
        public new int Percent { get; set; }

        [JsonProperty("source", Required = Required.Always)]
        public TraitTarget Source { get; set; }

        [JsonProperty("target", Required = Required.Always)]
        public TraitTarget Target { get; set; }
    }
}