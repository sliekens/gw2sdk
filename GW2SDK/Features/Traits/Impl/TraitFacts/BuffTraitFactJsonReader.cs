using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;
using static GW2SDK.Impl.JsonReaders.Mappings.MappingSignificance;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class BuffTraitFactJsonReader : TraitFactJsonReader<BuffTraitFact>
    {
        private BuffTraitFactJsonReader()
        {
        }

        public new static IJsonReader<BuffTraitFact> Instance { get; } = new BuffTraitFactJsonReader();

        protected override void ConfigureDerived(JsonObjectMapping<BuffTraitFact> traitFact)
        {
            var secondsReader = OptionalSecondsJsonReader.Instance;
            traitFact.Map("duration",    to => to.Duration,    secondsReader, Optional);
            traitFact.Map("status",      to => to.Status,      Optional);
            traitFact.Map("description", to => to.Description, Optional);
            traitFact.Map("apply_count", to => to.ApplyCount);
        }
    }
}
