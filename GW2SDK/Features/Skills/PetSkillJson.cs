using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public static class PetSkillJson
{
    public static PetSkill GetPetSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        OptionalMember<SkillFact> facts = new("facts");
        OptionalMember<TraitedSkillFact> traitedFacts = new("traited_facts");
        RequiredMember<string> description = new("description");
        OptionalMember<string> icon = new("icon");
        NullableMember<WeaponType> weaponType = new("weapon_type");
        OptionalMember<ProfessionName> professions = new("professions");
        NullableMember<SkillSlot> slot = new("slot");
        NullableMember<int> flipSkill = new("flip_skill");
        NullableMember<int> nextChain = new("next_chain");
        NullableMember<int> prevChain = new("prev_chain");
        RequiredMember<SkillFlag> flags = new("flags");
        NullableMember<int> specialization = new("specialization");
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Pet"))
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PetSkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts = facts.SelectMany(
                value => value.GetSkillFact(missingMemberBehavior, out _, out _)
            ),
            TraitedFacts =
                traitedFacts.SelectMany(value => value.GetTraitedSkillFact(missingMemberBehavior)),
            Description = description.GetValue(),
            Icon = icon.GetValueOrNull(),
            WeaponType = weaponType.GetValue(missingMemberBehavior),
            Professions = professions.GetValues(missingMemberBehavior),
            Slot = slot.GetValue(missingMemberBehavior),
            FlipSkill = flipSkill.GetValue(),
            NextChain = nextChain.GetValue(),
            PreviousChain = prevChain.GetValue(),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            Specialization = specialization.GetValue(),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }
}
