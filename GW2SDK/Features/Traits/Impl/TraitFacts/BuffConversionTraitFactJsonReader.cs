using System;
using System.Text.Json;
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
            traitFact.Map("percent", to => to.Percent);
            traitFact.Map("source", to => to.Source, (in JsonElement element, in JsonPath path) => Enum.Parse<TraitTarget>(element.GetString(), false));
            traitFact.Map("target", to => to.Target, (in JsonElement element, in JsonPath path) => Enum.Parse<TraitTarget>(element.GetString(), false));
        }
    }
}
