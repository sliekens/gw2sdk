using System;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Traits.Impl.TraitFacts;
using static GW2SDK.Impl.JsonReaders.Mappings.MappingSignificance;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitJsonReader : JsonObjectReader<Trait>
    {
        private readonly TraitFactDiscriminatingJsonReader _traitFactDiscriminatingJsonReader;
        private readonly UnexpectedPropertyBehavior _unexpectedPropertyBehavior;

        private TraitJsonReader(UnexpectedPropertyBehavior unexpectedPropertyBehavior)
        {
            _unexpectedPropertyBehavior = unexpectedPropertyBehavior;
            _traitFactDiscriminatingJsonReader = new TraitFactDiscriminatingJsonReader(_unexpectedPropertyBehavior);
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
            trait.Map("slot", to => to.Slot, (in JsonElement element, in JsonPath path) => Enum.Parse<TraitSlot>(element.GetString(), false));
            trait.Map("facts", to => to.Facts,
                (in JsonElement element, in JsonPath path) => _traitFactDiscriminatingJsonReader.Read(element, path), Optional);
            trait.Map("traited_facts", to => to.TraitedFacts, (in JsonElement element, in JsonPath path) => _traitFactDiscriminatingJsonReader.Read(element, path), Optional);
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
            traitSkill.Map("facts", to => to.Facts, (in JsonElement element, in JsonPath path) => _traitFactDiscriminatingJsonReader.Read(element, path));
            traitSkill.Map("traited_facts", to => to.TraitedFacts, (in JsonElement element, in JsonPath path) => _traitFactDiscriminatingJsonReader.Read(element, path), Optional);
            traitSkill.Map("description", to => to.Description);
            traitSkill.Map("icon", to => to.Icon);
            traitSkill.Map(
                "flags",
                to => to.Flags,
                (in JsonElement element, in JsonPath path) => Enum.Parse<TraitSkillFlag>(element.GetString(), false),
                Optional
            );
            traitSkill.Map(
                "categories",
                to => to.Categories,
                (in JsonElement element, in JsonPath path) => Enum.Parse<SkillCategoryName>(element.GetString(), false),
                Optional
            );
            traitSkill.Map("chat_link", to => to.ChatLink);
        }
    }
}
