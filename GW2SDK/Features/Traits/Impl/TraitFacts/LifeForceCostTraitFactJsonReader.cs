using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class LifeForceCostTraitFactJsonReader : TraitFactJsonReader<LifeForceCostTraitFact>
    {
        public new static IJsonReader<LifeForceCostTraitFact> Instance { get; } = new LifeForceCostTraitFactJsonReader();

        private LifeForceCostTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<LifeForceCostTraitFact> traitFact)
        {
            traitFact.Map("percent", to => to.Percent);
        }
    }
}