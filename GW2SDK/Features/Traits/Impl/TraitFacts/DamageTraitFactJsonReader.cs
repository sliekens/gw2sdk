using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class DamageTraitFactJsonReader : TraitFactJsonReader<DamageTraitFact>
    {
        public new static IJsonReader<DamageTraitFact> Instance { get; } = new DamageTraitFactJsonReader();

        private DamageTraitFactJsonReader()
        {
        }

        protected override void ConfigureDerived(JsonObjectMapping<DamageTraitFact> traitFact)
        {
            traitFact.Map("hit_count", to => to.HitCount);
            traitFact.Map("dmg_multiplier", to => to.DamageMultiplier);
        }
    }
}