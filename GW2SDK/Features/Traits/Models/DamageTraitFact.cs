using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record DamageTraitFact : TraitFact
    {
        public int HitCount { get; init; }

        public double DamageMultiplier { get; init; }
    }
}