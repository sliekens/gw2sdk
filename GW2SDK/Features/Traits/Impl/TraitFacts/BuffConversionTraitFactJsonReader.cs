using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class BuffConversionTraitFactJsonReader : TraitFactJsonReader<BuffConversionTraitFact>
    {
        private BuffConversionTraitFactJsonReader()
        {
        }

        public new static IJsonReader<BuffConversionTraitFact> Instance { get; } = new BuffConversionTraitFactJsonReader();

        protected override void ConfigureDerived(JsonObjectMapping<BuffConversionTraitFact> traitFact)
        {
            var traitTargetReader = new JsonStringEnumReader<TraitTarget>();
            traitFact.Map("percent", to => to.Percent);
            traitFact.Map("source",  to => to.Source, traitTargetReader);
            traitFact.Map("target",  to => to.Target, traitTargetReader);
        }
    }
}
