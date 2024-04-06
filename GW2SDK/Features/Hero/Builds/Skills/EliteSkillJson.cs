using System.Text.Json;
using GuildWars2.Hero.Training;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Skills;

internal static class EliteSkillJson
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
        RequiredMember weaponType = "weapon_type";
        RequiredMember professions = "professions";
        RequiredMember slot = "slot";
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
            else if (name.Match(member))
            {
                name = member;
            }
            else if (facts.Match(member))
            {
                facts = member;
            }
            else if (traitedFacts.Match(member))
            {
                traitedFacts = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (weaponType.Match(member))
            {
                weaponType = member;
            }
            else if (professions.Match(member))
            {
                professions = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (flipSkill.Match(member))
            {
                flipSkill = member;
            }
            else if (nextChain.Match(member))
            {
                nextChain = member;
            }
            else if (prevChain.Match(member))
            {
                prevChain = member;
            }
            else if (transformSkills.Match(member))
            {
                transformSkills = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (specialization.Match(member))
            {
                specialization = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (categories.Match(member))
            {
                categories = member;
            }
            else if (subskills.Match(member))
            {
                subskills = member;
            }
            else if (bundleSkills.Match(member))
            {
                bundleSkills = member;
            }
            else if (attunement.Match(member))
            {
                attunement = member;
            }
            else if (cost.Match(member))
            {
                cost = member;
            }
            else if (toolbeltSkill.Match(member))
            {
                toolbeltSkill = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        var professionRestrictions = professions.Map(
            values => values.GetList(value => value.GetEnum<ProfessionName>())
        );
        return new EliteSkill
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Facts =
                facts.Map(
                    values =>
                        values.GetList(value => value.GetFact(missingMemberBehavior, out _, out _))
                ),
            TraitedFacts =
                traitedFacts.Map(
                    values => values.GetList(value => value.GetTraitedFact(missingMemberBehavior))
                ),
            Description = description.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetString()) ?? "",
            WeaponType = weaponType.Map(value => value.GetWeaponType(missingMemberBehavior)),
            Professions =
                professionRestrictions.Count > 0
                    ? professionRestrictions
                    : Profession.AllProfessions,
            Slot = slot.Map(value => value.GetEnum<SkillSlot>()),
            FlipSkillId = flipSkill.Map(value => value.GetInt32()),
            NextSkillId = nextChain.Map(value => value.GetInt32()),
            PreviousSkillId = prevChain.Map(value => value.GetInt32()),
            TransformSkillIds =
                transformSkills.Map(values => values.GetList(value => value.GetInt32())),
            SkillFlags = flags.Map(value => value.GetSkillFlags()),
            SpecializationId = specialization.Map(value => value.GetInt32()),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            Categories =
                categories.Map(
                    values => values.GetList(value => value.GetEnum<SkillCategoryName>())
                )
                ?? Empty.List<Extensible<SkillCategoryName>>(),
            SubskillIds =
                subskills.Map(
                    values => values.GetList(value => value.GetSubskill(missingMemberBehavior))
                ),
            BundleSkillIds = bundleSkills.Map(values => values.GetList(value => value.GetInt32())),
            Attunement = attunement.Map(value => value.GetEnum<Attunement>()),
            Cost = cost.Map(value => value.GetInt32()),
            ToolbeltSkillId = toolbeltSkill.Map(value => value.GetInt32())
        };
    }
}
