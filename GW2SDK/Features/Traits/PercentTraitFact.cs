using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class PercentTraitFact : TraitFact
    {
        public double Percent { get; set; }
    }
}
