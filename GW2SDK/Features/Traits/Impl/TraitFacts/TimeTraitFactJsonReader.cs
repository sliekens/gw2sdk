using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class TimeTraitFactJsonReader : TraitFactJsonReader<TimeTraitFact>
    {
        public new static IJsonReader<TimeTraitFact> Instance { get; } = new TimeTraitFactJsonReader();

        private TimeTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<TimeTraitFact> traitFact)
        {
            traitFact.Map("duration", to => to.Duration, SecondsJsonReader.Instance);
        }
    }
}