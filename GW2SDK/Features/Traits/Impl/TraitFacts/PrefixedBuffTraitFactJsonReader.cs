using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;
using static GW2SDK.Impl.JsonReaders.Mappings.MappingSignificance;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class PrefixedBuffTraitFactJsonReader : TraitFactJsonReader<PrefixedBuffTraitFact>
    {
        private PrefixedBuffTraitFactJsonReader()
        {
        }

        public new static IJsonReader<PrefixedBuffTraitFact> Instance { get; } = new PrefixedBuffTraitFactJsonReader();

        protected override void ConfigureDerived(JsonObjectMapping<PrefixedBuffTraitFact> traitFact)
        {
            var secondsReader = OptionalSecondsJsonReader.Instance;
            traitFact.Map("duration", to => to.Duration, secondsReader, Optional);
            traitFact.Map("status", to => to.Status, Optional);
            traitFact.Map("description", to => to.Description, Optional);
            traitFact.Map("apply_count", to => to.ApplyCount);
            traitFact.Map("prefix", to => to.Prefix,
                prefix =>
                {
                    prefix.Map("text", to => to.Text);
                    prefix.Map("icon", to => to.Icon);
                    prefix.Map("status", to => to.Status, Optional);
                    prefix.Map("description", to => to.Description, Optional);
                });
        }
    }
}
