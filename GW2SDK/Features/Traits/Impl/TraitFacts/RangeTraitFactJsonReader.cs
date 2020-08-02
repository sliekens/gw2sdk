using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class RangeTraitFactJsonReader : TraitFactJsonReader<RangeTraitFact>
    {
        public new static IJsonReader<RangeTraitFact> Instance { get; } = new RangeTraitFactJsonReader();

        private RangeTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<RangeTraitFact> traitFact)
        {
            traitFact.Map("value", to => to.Value);
        }
    }
}