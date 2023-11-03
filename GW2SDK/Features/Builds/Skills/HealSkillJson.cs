using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds.Skills;

internal static class HealSkillJson
{
    public static HealSkill GetHealSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember facts = "facts";
        OptionalMember traitedFacts = "traited_facts";
        RequiredMember description = "description";
        OptionalMember icon = "icon";
        NullableMember weaponType = "weapon_type";
        OptionalMember professions = "professions";
        NullableMember slot = "slot";
        NullableMember flipSkill = "flip_skill";
        NullableMember nextChain = "next_chain";
        NullableMember prevChain = "prev_chain";
        RequiredMember flags = "flags";
        NullableMember specialization = "specialization";
        RequiredMember chatLink = "chat_link";
        OptionalMember categories = "categories";
        OptionalMember subskills = "subskills";
        OptionalMember bundleSkills = "bundle_skills";
        OptionalMember attunement = "attunement";
        NullableMember cost = "cost";
        NullableMember toolbeltSkill = "toolbelt_skill";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Heal"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == facts.Name)
            {
                facts = member;
            }
            else if (member.Name == traitedFacts.Name)
            {
                traitedFacts = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == weaponType.Name)
            {
                weaponType = member;
            }
            else if (member.Name == professions.Name)
            {
                professions = member;
            }
            else if (member.Name == slot.Name)
            {
                slot = member;
            }
            else if (member.Name == flipSkill.Name)
            {
                flipSkill = member;
            }
            else if (member.Name == nextChain.Name)
            {
                nextChain = member;
            }
            else if (member.Name == prevChain.Name)
            {
                prevChain = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == specialization.Name)
            {
                specialization = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == chatLink.Name)
            {
                chatLink = member;
            }
            else if (member.Name == categories.Name)
            {
                categories = member;
            }
            else if (member.Name == subskills.Name)
            {
                subskills = member;
            }
            else if (member.Name == bundleSkills.Name)
            {
                bundleSkills = member;
            }
            else if (member.Name == attunement.Name)
            {
                attunement = member;
            }
            else if (member.Name == cost.Name)
            {
                cost = member;
            }
            else if (member.Name == toolbeltSkill.Name)
            {
                toolbeltSkill = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HealSkill
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Facts =
                facts.Map(
                    values =>
                        values.GetList(
                            value => value.GetSkillFact(missingMemberBehavior, out _, out _)
                        )
                ),
            TraitedFacts =
                traitedFacts.Map(
                    values => values.GetList(
                        value => value.GetTraitedSkillFact(missingMemberBehavior)
                    )
                ),
            Description = description.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetString()),
            WeaponType = weaponType.Map(value => value.GetEnum<WeaponType>(missingMemberBehavior)),
            Professions =
                professions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ProfessionName>(missingMemberBehavior)
                        )
                ),
            Slot = slot.Map(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            FlipSkill = flipSkill.Map(value => value.GetInt32()),
            NextChain = nextChain.Map(value => value.GetInt32()),
            PreviousChain = prevChain.Map(value => value.GetInt32()),
            SkillFlag =
                flags.Map(
                    values => values.GetList(
                        value => value.GetEnum<SkillFlag>(missingMemberBehavior)
                    )
                ),
            Specialization = specialization.Map(value => value.GetInt32()),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            Categories =
                categories.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<SkillCategoryName>(missingMemberBehavior)
                        )
                ),
            Subskills =
                subskills.Map(
                    values => values.GetList(
                        value => value.GetSkillReference(missingMemberBehavior)
                    )
                ),
            BundleSkills = bundleSkills.Map(values => values.GetList(value => value.GetInt32())),
            Attunement = attunement.Map(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            Cost = cost.Map(value => value.GetInt32()),
            ToolbeltSkill = toolbeltSkill.Map(value => value.GetInt32())
        };
    }
}
