using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public static class EliteSkillJson
{
    public static EliteSkill GetEliteSkill(
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
        OptionalMember<int> transformSkills = new("transform_skills");
        RequiredMember<SkillFlag> flags = new("flags");
        NullableMember<int> specialization = new("specialization");
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");
        OptionalMember<SkillReference> subskills = new("subskills");
        OptionalMember<int> bundleSkills = new("bundle_skills");
        NullableMember<Attunement> attunement = new("attunement");
        NullableMember<int> cost = new("cost");
        NullableMember<int> toolbeltSkill = new("toolbelt_skill");

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
            else if (member.NameEquals(subskills.Name))
            {
                subskills.Value = member.Value;
            }
            else if (member.NameEquals(bundleSkills.Name))
            {
                bundleSkills.Value = member.Value;
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement.Value = member.Value;
            }
            else if (member.NameEquals(cost.Name))
            {
                cost.Value = member.Value;
            }
            else if (member.NameEquals(toolbeltSkill.Name))
            {
                toolbeltSkill.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EliteSkill
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
            TransformSkills = transformSkills.SelectMany(value => value.GetInt32()),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            Specialization = specialization.GetValue(),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior),
            Subskills =
                subskills.SelectMany(value => value.GetSkillReference(missingMemberBehavior)),
            BundleSkills = bundleSkills.SelectMany(value => value.GetInt32()),
            Attunement = attunement.GetValue(missingMemberBehavior),
            Cost = cost.GetValue(),
            ToolbeltSkill = toolbeltSkill.GetValue()
        };
    }
}
