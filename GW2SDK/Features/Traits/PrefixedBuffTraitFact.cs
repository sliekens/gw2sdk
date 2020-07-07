using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class PrefixedBuffTraitFact : BuffTraitFact
    {
        [JsonProperty("prefix", Required = Required.Always)]
        public BuffPrefix Prefix { get; set; } = new BuffPrefix();
    }
}