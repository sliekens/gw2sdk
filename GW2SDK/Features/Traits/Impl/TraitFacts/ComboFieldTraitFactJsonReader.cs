using System;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class ComboFieldTraitFactJsonReader : TraitFactJsonReader<ComboFieldTraitFact>
    {
        private ComboFieldTraitFactJsonReader()
        {
        }

        public new static IJsonReader<ComboFieldTraitFact> Instance { get; } = new ComboFieldTraitFactJsonReader();

        protected override void ConfigureDerived(JsonObjectMapping<ComboFieldTraitFact> traitFact)
        {
            traitFact.Map("field_type", to => to.Field, (in JsonElement element, in JsonPath path) => Enum.Parse<ComboFieldName>(element.GetString(), false));
        }
    }
}
