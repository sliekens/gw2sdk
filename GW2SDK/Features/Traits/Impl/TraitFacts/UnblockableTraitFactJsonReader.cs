using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class UnblockableTraitFactJsonReader : TraitFactJsonReader<UnblockableTraitFact>
    {
        public new static IJsonReader<UnblockableTraitFact> Instance { get; } = new UnblockableTraitFactJsonReader();

        private UnblockableTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<UnblockableTraitFact> traitFact)
        {
            traitFact.Map("value", to => to.Value);
        }
    }
}