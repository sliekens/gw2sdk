using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Traits.Impl.TraitFacts;
using static GW2SDK.Impl.JsonReaders.Mappings.MappingSignificance;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitJsonReader : JsonObjectReader<Trait>
    {
        private readonly JsonStringEnumReader<SkillCategoryName> _skillCategoryNameJsonReader;
        private readonly TraitFactDiscriminatingJsonReader _traitFactDiscriminatingJsonReader;
        private readonly JsonStringEnumReader<TraitSkillFlag> _traitSkillFlagJsonReader;
        private readonly JsonStringEnumReader<TraitSlot> _traitSlotJsonReader;
        private readonly UnexpectedPropertyBehavior _unexpectedPropertyBehavior;

        private TraitJsonReader(UnexpectedPropertyBehavior unexpectedPropertyBehavior)
        {
            _unexpectedPropertyBehavior = unexpectedPropertyBehavior;
            _traitSkillFlagJsonReader = new JsonStringEnumReader<TraitSkillFlag>();
            _traitSlotJsonReader = new JsonStringEnumReader<TraitSlot>();
            _traitFactDiscriminatingJsonReader = new TraitFactDiscriminatingJsonReader(_unexpectedPropertyBehavior);
            _skillCategoryNameJsonReader = new JsonStringEnumReader<SkillCategoryName>();
            Configure(MapTrait);
        }

        public static IJsonReader<Trait> Instance { get; } = new TraitJsonReader(UnexpectedPropertyBehavior.Error);

        private void MapTrait(JsonObjectMapping<Trait> trait)
        {
            trait.UnexpectedPropertyBehavior = _unexpectedPropertyBehavior;
            trait.Map("id", to => to.Id);
            trait.Map("tier", to => to.Tier);
            trait.Map("order", to => to.Order);
            trait.Map("name", to => to.Name);
            trait.Map("slot", to => to.Slot, _traitSlotJsonReader);
            trait.Map("facts", to => to.Facts, _traitFactDiscriminatingJsonReader, Optional);
            trait.Map("traited_facts", to => to.TraitedFacts, _traitFactDiscriminatingJsonReader, Optional);
            trait.Map("specialization", to => to.SpezializationId);
            trait.Map("icon", to => to.Icon);
            trait.Map("description", to => to.Description, Optional);
            trait.Map("skills", to => to.Skills, MapTraitSkill, Optional);
        }

        private void MapTraitSkill(JsonObjectMapping<TraitSkill> traitSkill)
        {
            traitSkill.UnexpectedPropertyBehavior = _unexpectedPropertyBehavior;
            traitSkill.Map("id", to => to.Id);
            traitSkill.Map("name", to => to.Name);
            traitSkill.Map("facts", to => to.Facts, _traitFactDiscriminatingJsonReader);
            traitSkill.Map("traited_facts", to => to.TraitedFacts, _traitFactDiscriminatingJsonReader, Optional);
            traitSkill.Map("description", to => to.Description);
            traitSkill.Map("icon", to => to.Icon);
            traitSkill.Map("flags", to => to.Flags, _traitSkillFlagJsonReader, Optional);
            traitSkill.Map("categories", to => to.Categories, _skillCategoryNameJsonReader, Optional);
            traitSkill.Map("chat_link", to => to.ChatLink);
        }
    }
}
