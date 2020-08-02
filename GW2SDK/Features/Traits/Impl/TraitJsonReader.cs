using GW2SDK.Impl.JsonReaders;
using GW2SDK.Traits.Impl.TraitFacts;
using static GW2SDK.Impl.JsonReaders.Mappings.MappingSignificance;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitJsonReader : JsonObjectReader<Trait>
    {
        public static JsonObjectReader<Trait> Instance { get; } = new TraitJsonReader(UnexpectedPropertyBehavior.Error);

        private TraitJsonReader(UnexpectedPropertyBehavior unexpectedPropertyBehavior)
        {
            Configure(
                trait =>
                {
                    trait.UnexpectedPropertyBehavior = unexpectedPropertyBehavior;
                    trait.Map("id", to => to.Id);
                    trait.Map("tier", to => to.Tier);
                    trait.Map("order", to => to.Order);
                    trait.Map("name", to => to.Name);
                    trait.Map("slot", to => to.Slot, new JsonStringEnumReader<TraitSlot>());
                    trait.Map("facts", to => to.Facts, new TraitFactDiscriminatingJsonReader(unexpectedPropertyBehavior), Optional);
                    trait.Map("traited_facts", to => to.TraitedFacts, new TraitFactDiscriminatingJsonReader(unexpectedPropertyBehavior), Optional);
                    trait.Map("specialization", to => to.SpezializationId);
                    trait.Map("icon", to => to.Icon);
                    trait.Map("description", to => to.Description, Optional);
                    trait.Map("skills", to => to.Skills, TraitSkillJsonReader.Instance, Optional);
                });
        }
    }
}
