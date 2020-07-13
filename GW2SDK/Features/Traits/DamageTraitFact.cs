using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DamageTraitFact : TraitFact
    {
        public int HitCount { get; set; }

        public double DamageMultiplier { get; set; }
    }
}