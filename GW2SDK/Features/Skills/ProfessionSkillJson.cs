using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

[PublicAPI]
public static class ProfessionSkillJson
{
    public static ProfessionSkill GetProfessionSkill(
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
        OptionalMember transformSkills = new("transform_skills");
        RequiredMember flags = new("flags");
        NullableMember specialization = new("specialization");
        RequiredMember chatLink = new("chat_link");
        OptionalMember categories = new("categories");
        NullableMember attunement = new("attunement");
        NullableMember cost = new("cost");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Profession"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(facts.Name))
            {
                facts.Value = member.Value;
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType.Value = member.Value;
            }
            else if (member.NameEquals(professions.Name))
            {
                professions.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill.Value = member.Value;
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain.Value = member.Value;
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain.Value = member.Value;
            }
            else if (member.NameEquals(transformSkills.Name))
            {
                transformSkills.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
            }
            else if (member.NameEquals(categories.Name))
            {
                categories.Value = member.Value;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement.Value = member.Value;
            }
            else if (member.NameEquals(cost.Name))
            {
                cost.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ProfessionSkill
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
            Slot = slot.Select(value => value.GetEnum<SkillSlot>(missingMemberBehavior)),
            FlipSkill = flipSkill.Select(value => value.GetInt32()),
            NextChain = nextChain.Select(value => value.GetInt32()),
            PreviousChain = prevChain.Select(value => value.GetInt32()),
            TransformSkills = transformSkills.SelectMany(value => value.GetInt32()),
            SkillFlag = flags.SelectMany(value => value.GetEnum<SkillFlag>(missingMemberBehavior)),
            Specialization = specialization.Select(value => value.GetInt32()),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Categories = categories.SelectMany(value => value.GetEnum<SkillCategoryName>(missingMemberBehavior)),
            Attunement = attunement.Select(value => value.GetEnum<Attunement>(missingMemberBehavior)),
            Cost = cost.Select(value => value.GetInt32())
        };
    }
}
