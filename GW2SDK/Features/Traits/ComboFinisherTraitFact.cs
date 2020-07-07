using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class ComboFinisherTraitFact : TraitFact
    {
        [JsonProperty("percent", Required = Required.Always)]
        public new int Percent { get; set; }

        [JsonProperty("finisher_type", Required = Required.Always)]
        public ComboFinisherName FinisherName { get; set; }
    }
}