using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class ComboFieldTraitFactJsonReader : TraitFactJsonReader<ComboFieldTraitFact>
    {
        public new static IJsonReader<ComboFieldTraitFact> Instance { get; } = new ComboFieldTraitFactJsonReader();

        private ComboFieldTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<ComboFieldTraitFact> traitFact)
        {
            traitFact.Map("field_type", to => to.Field, new JsonStringEnumReader<ComboFieldName>());
        }
    }
}