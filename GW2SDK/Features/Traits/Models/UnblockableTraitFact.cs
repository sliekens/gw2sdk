using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record UnblockableTraitFact : TraitFact
    {
        public bool Value { get; init; }
    }
}