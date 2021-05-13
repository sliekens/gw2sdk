using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record BuffConversionTraitFact : TraitFact
    {
        public int Percent { get; init; }

        public TraitTarget Source { get; init; }

        public TraitTarget Target { get; init; }
    }
}