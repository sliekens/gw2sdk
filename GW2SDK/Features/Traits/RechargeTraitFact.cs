using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class RechargeTraitFact : TraitFact
    {
        public double Value { get; set; }
    }
}
