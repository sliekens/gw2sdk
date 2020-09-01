using System;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class ComboFinisherTraitFactJsonReader : TraitFactJsonReader<ComboFinisherTraitFact>
    {
        private ComboFinisherTraitFactJsonReader()
        {
        }

        public new static IJsonReader<ComboFinisherTraitFact> Instance { get; } = new ComboFinisherTraitFactJsonReader();

        protected override void ConfigureDerived(JsonObjectMapping<ComboFinisherTraitFact> traitFact)
        {
            traitFact.Map("percent", to => to.Percent);
            traitFact.Map(
                "finisher_type",
                to => to.FinisherName,
                (in JsonElement element, in JsonPath path) => Enum.Parse<ComboFinisherName>(element.GetString(), false)
            );
        }
    }
}
