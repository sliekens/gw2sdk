using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class RadiusTraitFactJsonReader : TraitFactJsonReader<RadiusTraitFact>
    {
        public new static IJsonReader<RadiusTraitFact> Instance { get; } = new RadiusTraitFactJsonReader();

        private RadiusTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<RadiusTraitFact> traitFact)
        {
            traitFact.Map("distance", to => to.Distance);
        }
    }
}