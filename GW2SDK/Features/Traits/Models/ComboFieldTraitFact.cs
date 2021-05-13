using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ComboFieldTraitFact : TraitFact
    {
        public ComboFieldName Field { get; init; }
    }
}