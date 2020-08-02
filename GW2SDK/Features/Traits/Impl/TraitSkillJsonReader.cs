using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Traits.Impl.TraitFacts;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitSkillJsonReader : JsonObjectReader<TraitSkill>
    {
        private TraitSkillJsonReader()
        {
            Configure(
                traitSkill =>
                {
                    traitSkill.Name = "$.skills[*]";
                    traitSkill.Map("id",    to => to.Id);
                    traitSkill.Map("name",  to => to.Name);
                    traitSkill.Map("facts", to => to.Facts, new TraitFactDiscriminatingJsonReader(traitSkill.UnexpectedPropertyBehavior));
                    traitSkill.Map(
                        "traited_facts",
                        to => to.TraitedFacts,
                        new TraitFactDiscriminatingJsonReader(traitSkill.UnexpectedPropertyBehavior),
                        MappingSignificance.Optional
                    );
                    traitSkill.Map("description", to => to.Description);
                    traitSkill.Map("icon",        to => to.Icon);
                    traitSkill.Map("flags",       to => to.Flags,      new JsonStringEnumReader<TraitSkillFlag>(),    MappingSignificance.Optional);
                    traitSkill.Map("categories",  to => to.Categories, new JsonStringEnumReader<SkillCategoryName>(), MappingSignificance.Optional);
                    traitSkill.Map("chat_link",   to => to.ChatLink);
                }
            );
        }

        public static IJsonReader<TraitSkill> Instance { get; } = new TraitSkillJsonReader();
    }
}
