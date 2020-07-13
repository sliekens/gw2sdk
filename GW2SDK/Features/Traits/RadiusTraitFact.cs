using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class RadiusTraitFact : TraitFact
    {
        public int Distance { get; set; }
    }
}
