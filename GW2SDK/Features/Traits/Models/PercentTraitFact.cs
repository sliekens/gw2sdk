using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record PercentTraitFact : TraitFact
    {
        public double Percent { get; init; }
    }
}