using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ComboFinisherTraitFact : TraitFact
    {
        public int Percent { get; init; }

        public ComboFinisherName FinisherName { get; init; }
    }
}