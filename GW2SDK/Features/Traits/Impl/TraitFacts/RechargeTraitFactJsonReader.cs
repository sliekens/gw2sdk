using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class RechargeTraitFactJsonReader : TraitFactJsonReader<RechargeTraitFact>
    {
        public new static IJsonReader<RechargeTraitFact> Instance { get; } = new RechargeTraitFactJsonReader();

        private RechargeTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<RechargeTraitFact> traitFact)
        {
            traitFact.Map("value", to => to.Value);
        }
    }
}