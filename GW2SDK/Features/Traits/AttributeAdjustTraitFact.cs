using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class AttributeAdjustTraitFact : TraitFact
    {
        public int Value { get; set; }

        public TraitTarget Target { get; set; }
    }
}
