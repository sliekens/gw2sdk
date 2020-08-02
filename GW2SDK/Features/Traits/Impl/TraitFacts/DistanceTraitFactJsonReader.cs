using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class DistanceTraitFactJsonReader : TraitFactJsonReader<DistanceTraitFact>
    {
        public new static IJsonReader<DistanceTraitFact> Instance { get; } = new DistanceTraitFactJsonReader();

        private DistanceTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<DistanceTraitFact> traitFact)
        {
            traitFact.Map("distance", to => to.Distance);
        }
    }
}