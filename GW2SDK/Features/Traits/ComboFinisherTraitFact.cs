using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class ComboFinisherTraitFact : TraitFact
    {
        public int Percent { get; set; }

        public ComboFinisherName FinisherName { get; set; }
    }
}
