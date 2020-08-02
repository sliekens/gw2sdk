using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class PercentTraitFactJsonReader : TraitFactJsonReader<PercentTraitFact>
    {
        public new static IJsonReader<PercentTraitFact> Instance { get; } = new PercentTraitFactJsonReader();

        private PercentTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<PercentTraitFact> traitFact)
        {
            traitFact.Map("percent", to => to.Percent);
        }
    }
}