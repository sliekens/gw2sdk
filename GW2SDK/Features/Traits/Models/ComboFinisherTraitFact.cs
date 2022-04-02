using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    public sealed record ComboFinisherTraitFact : TraitFact
    {
        public int Percent { get; init; }

        public ComboFinisherName FinisherName { get; init; }
    }
}
