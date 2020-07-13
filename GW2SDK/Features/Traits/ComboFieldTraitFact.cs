using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class ComboFieldTraitFact : TraitFact
    {
        public ComboFieldName Field { get; set; }
    }
}
