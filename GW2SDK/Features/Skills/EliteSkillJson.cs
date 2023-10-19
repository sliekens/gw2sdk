using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

[PublicAPI]
public static class EliteSkillJson
{
    public static EliteSkill GetEliteSkill(
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
        OptionalMember transformSkills = "transform_skills";
        RequiredMember flags = "flags";
        NullableMember specialization = "specialization";
        RequiredMember chatLink = "chat_link";
        OptionalMember categories = "categories";
        OptionalMember subskills = "subskills";
        OptionalMember bundleSkills = "bundle_skills";
        NullableMember attunement = "attunement";
        NullableMember cost = "cost";
        NullableMember toolbeltSkill = "toolbelt_skill";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Elite"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = member;
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = member;
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = member;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = member;
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = member;
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = member;
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = member;
            }
            else if (member.NameEquals(transformSkills.Name))
            {
                transformSkills = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = member;
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = member;
            }
            else if (member.NameEquals(subskills.Name))
            {
                subskills = member;
            }
            else if (member.NameEquals(bundleSkills.Name))
            {
                bundleSkills = member;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = member;
            }
            else if (member.NameEquals(cost.Name))
            {
                cost = member;
            }
            else if (member.NameEquals(toolbeltSkill.Name))
            {
                toolbeltSkill = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EliteSkill
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Facts = facts.Select(
                values => values.GetList(value => value.GetSkillFact(missingMemberBehavior, out _, out _))
            ),
            TraitedFacts =
                traitedFacts.Select(values => values.GetList(value => value.GetTraitedSkillFact(missingMemberBehavior))),
            Description = description.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetString()),
            WeaponType = weaponType.Select(value => value.GetEnum<WeaponType>(missingMemberBehavior)),
            Professions = professions.Select(values => values.GetList(value => value.GetEnum<ProfessionName>(missingMemberBehavior))),
            Slot = slot.Select(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            FlipSkill = flipSkill.Select(value => value.GetInt32()),
            NextChain = nextChain.Select(value => value.GetInt32()),
            PreviousChain = prevChain.Select(value => value.GetInt32()),
            TransformSkills = transformSkills.Select(values => values.GetList(value => value.GetInt32())),
            SkillFlag = flags.Select(values => values.GetList(value => value.GetEnum<SkillFlag>(missingMemberBehavior))),
            Specialization = specialization.Select(value => value.GetInt32()),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Categories = categories.Select(values => values.GetList(value => value.GetEnum<SkillCategoryName>(missingMemberBehavior))),
            Subskills =
                subskills.Select(values => values.GetList(value => value.GetSkillReference(missingMemberBehavior))),
            BundleSkills = bundleSkills.Select(values => values.GetList(value => value.GetInt32())),
            Attunement = attunement.Select(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            Cost = cost.Select(value => value.GetInt32()),
            ToolbeltSkill = toolbeltSkill.Select(value => value.GetInt32())
        };
    }
}
