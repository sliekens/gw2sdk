using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record RechargeTraitFact : TraitFact
    {
        public double Value { get; init; }
    }
}
