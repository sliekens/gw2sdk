using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class NoDataTraitFactJsonReader : TraitFactJsonReader<NoDataTraitFact>
    {
        public new static IJsonReader<NoDataTraitFact> Instance { get; } = new NoDataTraitFactJsonReader();

        private NoDataTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<NoDataTraitFact> traitFact)
        {
        }
    }
}