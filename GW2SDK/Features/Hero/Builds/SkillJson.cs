﻿using System.Text.Json;
using GuildWars2.Hero.Builds.Skills;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SkillJson
{
    public static Skill GetSkill(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // Unlike most models with a 'type' property, skills don't have always have it
        if (json.TryGetProperty("type", out var type))
        {
            switch (type.GetString())
            {
                case "Bundle":
                    return json.GetBundleSkill(missingMemberBehavior);
                case "Elite":
                    return json.GetEliteSkill(missingMemberBehavior);
                case "Heal":
                    return json.GetHealSkill(missingMemberBehavior);
                case "Monster":
                    return json.GetMonsterSkill(missingMemberBehavior);
                case "Pet":
                    return json.GetPetSkill(missingMemberBehavior);
                case "Profession":
                    return json.GetProfessionSkill(missingMemberBehavior);
                case "Toolbelt":
                    return json.GetToolbeltSkill(missingMemberBehavior);
                case "Transform":
                    return json.GetLockedSkill(missingMemberBehavior);
                case "Utility":
                    return json.GetUtilitySkill(missingMemberBehavior);
                case "Weapon":
                    return json.GetWeaponSkill(missingMemberBehavior);
            }
        }

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

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Skill
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
            IconHref = icon.Map(value => value.GetString()),
            WeaponType = weaponType.Map(value => value.GetWeaponType(missingMemberBehavior)),
            Professions =
                professions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ProfessionName>(missingMemberBehavior)
                        )
                ),
            Slot = slot.Map(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            FlipSkillId = flipSkill.Map(value => value.GetInt32()),
            NextSkillId = nextChain.Map(value => value.GetInt32()),
            PreviousSkillId = prevChain.Map(value => value.GetInt32()),
            SkillFlags = flags.Map(value => value.GetSkillFlags()),
            SpecializationId = specialization.Map(value => value.GetInt32()),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            Categories = categories.Map(
                values => values.GetList(
                    value => value.GetEnum<SkillCategoryName>(missingMemberBehavior)
                )
            )
        };
    }
}
