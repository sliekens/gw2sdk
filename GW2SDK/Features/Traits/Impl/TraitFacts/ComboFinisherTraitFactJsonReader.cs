using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class ComboFinisherTraitFactJsonReader : TraitFactJsonReader<ComboFinisherTraitFact>
    {
        public new static IJsonReader<ComboFinisherTraitFact> Instance { get; } = new ComboFinisherTraitFactJsonReader();

        private ComboFinisherTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<ComboFinisherTraitFact> traitFact)
        {
            traitFact.Map("percent", to => to.Percent);
            traitFact.Map("finisher_type", to => to.FinisherName, new JsonStringEnumReader<ComboFinisherName>());
        }
    }
}