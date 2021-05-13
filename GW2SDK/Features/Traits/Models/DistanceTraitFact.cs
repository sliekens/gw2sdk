using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record DistanceTraitFact : TraitFact
    {
        public int Distance { get; init; }
    }
}