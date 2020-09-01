using System;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class TimeTraitFactJsonReader : TraitFactJsonReader<TimeTraitFact>
    {
        private TimeTraitFactJsonReader()
        {
        }

        public new static IJsonReader<TimeTraitFact> Instance { get; } = new TimeTraitFactJsonReader();

        protected override void ConfigureDerived(JsonObjectMapping<TimeTraitFact> traitFact)
        {
            traitFact.Map("duration", to => to.Duration, (in JsonElement element, in JsonPath path) => TimeSpan.FromSeconds(element.GetDouble()));
        }
    }
}
