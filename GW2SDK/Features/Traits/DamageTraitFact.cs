using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DamageTraitFact : TraitFact
    {
        [JsonProperty("hit_count", Required = Required.Always)]
        public int HitCount { get; set; }

        [JsonProperty("dmg_multiplier", Required = Required.Always)]
        public double DamageMultiplier { get; set; }
    }
}