using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

[PublicAPI]
public static class WeaponSkillJson
{
    public static WeaponSkill GetWeaponSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        OptionalMember facts = new("facts");
        OptionalMember traitedFacts = new("traited_facts");
        RequiredMember description = new("description");
        OptionalMember icon = new("icon");
        NullableMember weaponType = new("weapon_type");
        OptionalMember professions = new("professions");
        NullableMember slot = new("slot");
        NullableMember flipSkill = new("flip_skill");
        NullableMember nextChain = new("next_chain");
        NullableMember prevChain = new("prev_chain");
        RequiredMember flags = new("flags");
        NullableMember specialization = new("specialization");
        RequiredMember chatLink = new("chat_link");
        NullableMember attunement = new("attunement");
        NullableMember dualAttunement = new("dual_attunement");
        OptionalMember categories = new("categories");
        NullableMember cost = new("cost");
        NullableMember offhand = new("dual_wield");
        NullableMember initiative = new("initiative");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Weapon"))
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
            else if (member.NameEquals(attunement.Name))
            {
                attunement = member;
            }
            else if (member.NameEquals(dualAttunement.Name))
            {
                dualAttunement = member;
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization = member;
            }
            else if (member.NameEquals(cost.Name))
            {
                cost = member;
            }
            else if (member.NameEquals(offhand.Name))
            {
                offhand = member;
            }
            else if (member.NameEquals(initiative.Name))
            {
                initiative = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponSkill
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Facts = facts.SelectMany(
                value => value.GetSkillFact(missingMemberBehavior, out _, out _)
            ),
            TraitedFacts =
                traitedFacts.SelectMany(value => value.GetTraitedSkillFact(missingMemberBehavior)),
            Description = description.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetString()),
            WeaponType = weaponType.Select(value => value.GetEnum<WeaponType>(missingMemberBehavior)),
            Professions = professions.SelectMany(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Attunement = attunement.Select(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            DualAttunement = dualAttunement.Select(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            Slot = slot.Select(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            FlipSkill = flipSkill.Select(value => value.GetInt32()),
            NextChain = nextChain.Select(value => value.GetInt32()),
            PreviousChain = prevChain.Select(value => value.GetInt32()),
            SkillFlag = flags.SelectMany(value => value.GetEnum<SkillFlag>(missingMemberBehavior)),
            Specialization = specialization.Select(value => value.GetInt32()),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Categories = categories.SelectMany(value => value.GetEnum<SkillCategoryName>(missingMemberBehavior)),
            Cost = cost.Select(value => value.GetInt32()),
            Offhand = offhand.Select(value => value.GetEnum<Offhand>(missingMemberBehavior)),
            Initiative = initiative.Select(value => value.GetInt32())
        };
    }
}
