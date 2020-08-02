using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class StunBreakTraitFactJsonReader : TraitFactJsonReader<StunBreakTraitFact>
    {
        public new static IJsonReader<StunBreakTraitFact> Instance { get; } = new StunBreakTraitFactJsonReader();

        private StunBreakTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<StunBreakTraitFact> traitFact)
        {
            traitFact.Map("value", to => to.Value);
        }
    }
}