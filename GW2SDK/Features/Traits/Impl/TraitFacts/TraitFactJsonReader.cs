using GW2SDK.Annotations;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    [Inheritable]
    internal class TraitFactJsonReader<T> : JsonObjectReader<T> where T : TraitFact
    {
        protected TraitFactJsonReader()
        {
            Configure(MapTraitFact);
        }

        public static IJsonReader<T> Instance { get; } = new TraitFactJsonReader<T>();

        private void MapTraitFact(JsonObjectMapping<T> traitFact)
        {
            traitFact.Ignore("type");
            traitFact.Map("text", to => to.Text, MappingSignificance.Optional);
            traitFact.Map("icon", to => to.Icon);
            traitFact.Map("requires_trait", to => to.RequiresTrait);
            traitFact.Map("overrides", to => to.Overrides);
            ConfigureDerived(traitFact);
        }

        protected virtual void ConfigureDerived(JsonObjectMapping<T> traitFact)
        {
        }
    }
}
