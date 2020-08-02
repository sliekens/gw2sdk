using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class NumberTraitFactJsonReader : TraitFactJsonReader<NumberTraitFact>
    {
        public new static IJsonReader<NumberTraitFact> Instance { get; } = new NumberTraitFactJsonReader();

        private NumberTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<NumberTraitFact> traitFact)
        {
            traitFact.Map("value", to => to.Value);
        }
    }
}