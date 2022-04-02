using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record RangeTraitFact : TraitFact
    {
        public int Value { get; init; }
    }
}
