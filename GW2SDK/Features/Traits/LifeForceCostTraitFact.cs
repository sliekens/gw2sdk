using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    /// <summary>Sometimes used to indicate the life force cost for the Necromancer's Shroud skills.</summary>
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class LifeForceCostTraitFact : TraitFact
    {
        public int Percent { get; set; }
    }
}
