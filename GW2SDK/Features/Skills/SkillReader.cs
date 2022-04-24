using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public static class SkillReader
{
    public static Skill GetSkill(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // Unlike most models with a 'type' property, skills don't have always have it
        if (json.TryGetProperty("type", out var type))
        {
            switch (type.GetString())
            {
                case "Bundle":
                    return ReadBundleSkill(json, missingMemberBehavior);
                case "Elite":
                    return ReadEliteSkill(json, missingMemberBehavior);
                case "Heal":
                    return ReadHealSkill(json, missingMemberBehavior);
                case "Monster":
                    return ReadMonsterSkill(json, missingMemberBehavior);
                case "Pet":
                    return ReadPetSkill(json, missingMemberBehavior);
                case "Profession":
                    return ReadProfessionSkill(json, missingMemberBehavior);
                case "Toolbelt":
                    return ReadToolbeltSkill(json, missingMemberBehavior);
                case "Transform":
                    return ReadTransformSkill(json, missingMemberBehavior);
                case "Utility":
                    return ReadUtilitySkill(json, missingMemberBehavior);
                case "Weapon":
                    return ReadWeaponSkill(json, missingMemberBehavior);
            }
        }

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
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");

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
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Skill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
            Description = description.GetValue(),
            Icon = icon.GetValueOrNull(),
            WeaponType = weaponType.GetValue(missingMemberBehavior),
            Professions = professions.GetValues(missingMemberBehavior),
            Slot = slot.GetValue(missingMemberBehavior),
            FlipSkill = flipSkill.GetValue(),
            NextChain = nextChain.GetValue(),
            PreviousChain = prevChain.GetValue(),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }

    private static BundleSkill ReadBundleSkill(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
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
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Bundle"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BundleSkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
            Description = description.GetValue(),
            Icon = icon.GetValueOrNull(),
            WeaponType = weaponType.GetValue(missingMemberBehavior),
            Professions = professions.GetValues(missingMemberBehavior),
            Slot = slot.GetValue(missingMemberBehavior),
            FlipSkill = flipSkill.GetValue(),
            NextChain = nextChain.GetValue(),
            PreviousChain = prevChain.GetValue(),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }

    private static EliteSkill ReadEliteSkill(
        JsonElement json,
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
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(transformSkills.Name))
            {
                transformSkills = transformSkills.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization = specialization.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (member.NameEquals(subskills.Name))
            {
                subskills = subskills.From(member.Value);
            }
            else if (member.NameEquals(bundleSkills.Name))
            {
                bundleSkills = bundleSkills.From(member.Value);
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = attunement.From(member.Value);
            }
            else if (member.NameEquals(cost.Name))
            {
                cost = cost.From(member.Value);
            }
            else if (member.NameEquals(toolbeltSkill.Name))
            {
                toolbeltSkill = toolbeltSkill.From(member.Value);
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
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
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
            Subskills = subskills.SelectMany(value => ReadSubskill(value, missingMemberBehavior)),
            BundleSkills = bundleSkills.SelectMany(value => value.GetInt32()),
            Attunement = attunement.GetValue(missingMemberBehavior),
            Cost = cost.GetValue(),
            ToolbeltSkill = toolbeltSkill.GetValue()
        };
    }

    private static HealSkill ReadHealSkill(
        JsonElement json,
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
        OptionalMember<SkillReference> subskills = new("subskills");
        OptionalMember<int> bundleSkills = new("bundle_skills");
        OptionalMember<Attunement> attunement = new("attunement");
        NullableMember<int> cost = new("cost");
        NullableMember<int> toolbeltSkill = new("toolbelt_skill");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Heal"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization = specialization.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (member.NameEquals(subskills.Name))
            {
                subskills = subskills.From(member.Value);
            }
            else if (member.NameEquals(bundleSkills.Name))
            {
                bundleSkills = bundleSkills.From(member.Value);
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = attunement.From(member.Value);
            }
            else if (member.NameEquals(cost.Name))
            {
                cost = cost.From(member.Value);
            }
            else if (member.NameEquals(toolbeltSkill.Name))
            {
                toolbeltSkill = toolbeltSkill.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HealSkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
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
            Categories = categories.GetValues(missingMemberBehavior),
            Subskills = subskills.SelectMany(value => ReadSubskill(value, missingMemberBehavior)),
            BundleSkills = bundleSkills.SelectMany(value => value.GetInt32()),
            Attunement = attunement.GetValue(missingMemberBehavior),
            Cost = cost.GetValue(),
            ToolbeltSkill = toolbeltSkill.GetValue()
        };
    }

    private static MonsterSkill ReadMonsterSkill(
        JsonElement json,
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
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Monster"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MonsterSkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
            Description = description.GetValue(),
            Icon = icon.GetValueOrNull(),
            WeaponType = weaponType.GetValue(missingMemberBehavior),
            Professions = professions.GetValues(missingMemberBehavior),
            Slot = slot.GetValue(missingMemberBehavior),
            FlipSkill = flipSkill.GetValue(),
            NextChain = nextChain.GetValue(),
            PreviousChain = prevChain.GetValue(),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }

    private static PetSkill ReadPetSkill(
        JsonElement json,
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
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
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
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
            Description = description.GetValue(),
            Icon = icon.GetValueOrNull(),
            WeaponType = weaponType.GetValue(missingMemberBehavior),
            Professions = professions.GetValues(missingMemberBehavior),
            Slot = slot.GetValue(missingMemberBehavior),
            FlipSkill = flipSkill.GetValue(),
            NextChain = nextChain.GetValue(),
            PreviousChain = prevChain.GetValue(),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }

    private static ProfessionSkill ReadProfessionSkill(
        JsonElement json,
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
        NullableMember<Attunement> attunement = new("attunement");
        NullableMember<int> cost = new("cost");

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
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(transformSkills.Name))
            {
                transformSkills = transformSkills.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization = specialization.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = attunement.From(member.Value);
            }
            else if (member.NameEquals(cost.Name))
            {
                cost = cost.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ProfessionSkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
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
            Attunement = attunement.GetValue(missingMemberBehavior),
            Cost = cost.GetValue()
        };
    }

    private static ToolbeltSkill ReadToolbeltSkill(
        JsonElement json,
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
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Toolbelt"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ToolbeltSkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
            Description = description.GetValue(),
            Icon = icon.GetValueOrNull(),
            WeaponType = weaponType.GetValue(missingMemberBehavior),
            Professions = professions.GetValues(missingMemberBehavior),
            Slot = slot.GetValue(missingMemberBehavior),
            FlipSkill = flipSkill.GetValue(),
            NextChain = nextChain.GetValue(),
            PreviousChain = prevChain.GetValue(),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }

    private static TransformSkill ReadTransformSkill(
        JsonElement json,
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
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Transform"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TransformSkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
            Description = description.GetValue(),
            Icon = icon.GetValueOrNull(),
            WeaponType = weaponType.GetValue(missingMemberBehavior),
            Professions = professions.GetValues(missingMemberBehavior),
            Slot = slot.GetValue(missingMemberBehavior),
            FlipSkill = flipSkill.GetValue(),
            NextChain = nextChain.GetValue(),
            PreviousChain = prevChain.GetValue(),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }

    private static UtilitySkill ReadUtilitySkill(
        JsonElement json,
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
        OptionalMember<SkillReference> subskills = new("subskills");
        OptionalMember<int> bundleSkills = new("bundle_skills");
        NullableMember<Attunement> attunement = new("attunement");
        NullableMember<int> toolbeltSkill = new("toolbelt_skill");
        NullableMember<int> cost = new("cost");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Utility"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization = specialization.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (member.NameEquals(subskills.Name))
            {
                subskills = subskills.From(member.Value);
            }
            else if (member.NameEquals(bundleSkills.Name))
            {
                bundleSkills = bundleSkills.From(member.Value);
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = attunement.From(member.Value);
            }
            else if (member.NameEquals(toolbeltSkill.Name))
            {
                toolbeltSkill = toolbeltSkill.From(member.Value);
            }
            else if (member.NameEquals(cost.Name))
            {
                cost = cost.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UtilitySkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
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
            Categories = categories.GetValues(missingMemberBehavior),
            Subskills = subskills.SelectMany(value => ReadSubskill(value, missingMemberBehavior)),
            BundleSkills = bundleSkills.SelectMany(value => value.GetInt32()),
            Attunement = attunement.GetValue(missingMemberBehavior),
            ToolbeltSkill = toolbeltSkill.GetValue(),
            Cost = cost.GetValue()
        };
    }

    private static WeaponSkill ReadWeaponSkill(
        JsonElement json,
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
        RequiredMember<string> chatLink = new("chat_link");
        NullableMember<Attunement> attunement = new("attunement");
        NullableMember<Attunement> dualAttunement = new("dual_attunement");
        NullableMember<int> specialization = new("specialization");
        OptionalMember<SkillCategoryName> categories = new("categories");
        NullableMember<int> cost = new("cost");
        NullableMember<Offhand> offhand = new("dual_wield");
        NullableMember<int> initiative = new("initiative");

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
                name = name.From(member.Value);
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = facts.From(member.Value);
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = traitedFacts.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(weaponType.Name))
            {
                weaponType = weaponType.From(member.Value);
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = professions.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(flipSkill.Name))
            {
                flipSkill = flipSkill.From(member.Value);
            }
            else if (member.NameEquals(nextChain.Name))
            {
                nextChain = nextChain.From(member.Value);
            }
            else if (member.NameEquals(prevChain.Name))
            {
                prevChain = prevChain.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = categories.From(member.Value);
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = attunement.From(member.Value);
            }
            else if (member.NameEquals(dualAttunement.Name))
            {
                dualAttunement = dualAttunement.From(member.Value);
            }
            else if (member.NameEquals(specialization.Name))
            {
                specialization = specialization.From(member.Value);
            }
            else if (member.NameEquals(cost.Name))
            {
                cost = cost.From(member.Value);
            }
            else if (member.NameEquals(offhand.Name))
            {
                offhand = offhand.From(member.Value);
            }
            else if (member.NameEquals(initiative.Name))
            {
                initiative = initiative.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponSkill
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Facts =
                facts.SelectMany(
                    value => ReadSkillFact(value, missingMemberBehavior, out _, out _)
                    ),
            TraitedFacts =
                traitedFacts.SelectMany(
                    value => ReadTraitedSkillFact(value, missingMemberBehavior)
                    ),
            Description = description.GetValue(),
            Icon = icon.GetValueOrNull(),
            WeaponType = weaponType.GetValue(missingMemberBehavior),
            Professions = professions.GetValues(missingMemberBehavior),
            Specialization = specialization.GetValue(),
            Attunement = attunement.GetValue(missingMemberBehavior),
            DualAttunement = dualAttunement.GetValue(missingMemberBehavior),
            Slot = slot.GetValue(missingMemberBehavior),
            FlipSkill = flipSkill.GetValue(),
            NextChain = nextChain.GetValue(),
            PreviousChain = prevChain.GetValue(),
            SkillFlag = flags.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior),
            Cost = cost.GetValue(),
            Offhand = offhand.GetValue(missingMemberBehavior),
            Initiative = initiative.GetValue()
        };
    }

    private static TraitedSkillFact ReadTraitedSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var fact = ReadSkillFact(
            json,
            missingMemberBehavior,
            out var requiresTrait,
            out var overrides
            );
        return new TraitedSkillFact
        {
            Fact = fact,
            RequiresTrait = requiresTrait.GetValueOrDefault(),
            Overrides = overrides
        };
    }

    private static SkillFact ReadSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        // BUG: Life Force Cost is missing a type property but we can treat it as Percent
        if (!json.TryGetProperty("type", out var type) && json.TryGetProperty("percent", out _))
        {
            return ReadPercentSkillFact(
                json,
                missingMemberBehavior,
                out requiresTrait,
                out overrides
                );
        }

        switch (type.GetString())
        {
            case "AttributeAdjust":
                return ReadAttributeAdjustSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "ComboField":
                return ReadComboFieldSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "ComboFinisher":
                return ReadComboFinisherSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Buff":
                return ReadBuffSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Damage":
                return ReadDamageSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Distance":
                return ReadDistanceSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Duration":
                return ReadDurationSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "HealingAdjust":
                return ReadHealingAdjustSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "NoData":
                return ReadNoDataSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Number":
                return ReadNumberSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Percent":
                return ReadPercentSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "PrefixedBuff":
                return ReadPrefixedBuffSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Radius":
                return ReadRadiusSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Range":
                return ReadRangeSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Recharge":
                return ReadRechargeSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "StunBreak":
                return ReadStunBreakSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Time":
                return ReadTimeSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
            case "Unblockable":
                return ReadUnblockableSkillFact(
                    json,
                    missingMemberBehavior,
                    out requiresTrait,
                    out overrides
                    );
        }

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");

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
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue()
        };
    }

    private static RadiusSkillFact ReadRadiusSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> distance = new("distance");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Radius"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(distance.Name))
            {
                distance = distance.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RadiusSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Distance = distance.GetValue()
        };
    }

    private static RangeSkillFact ReadRangeSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> value = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Range"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(value.Name))
            {
                value = value.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RangeSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Value = value.GetValue()
        };
    }

    private static RechargeSkillFact ReadRechargeSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<double> value = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Recharge"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(value.Name))
            {
                value = value.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RechargeSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Value = value.GetValue()
        };
    }

    private static StunBreakSkillFact ReadStunBreakSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<bool> value = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("StunBreak"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(value.Name))
            {
                value = value.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new StunBreakSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Value = value.GetValue()
        };
    }

    private static TimeSkillFact ReadTimeSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<TimeSpan> duration = new("duration");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Time"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(duration.Name))
            {
                duration = duration.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TimeSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble()))
        };
    }

    private static UnblockableSkillFact ReadUnblockableSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<bool> value = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Unblockable"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(value.Name))
            {
                value = value.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UnblockableSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Value = value.GetValue()
        };
    }

    private static PrefixedBuffSkillFact ReadPrefixedBuffSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        NullableMember<TimeSpan> duration = new("duration");
        OptionalMember<string> status = new("status");
        OptionalMember<string> description = new("description");
        NullableMember<int> applyCount = new("apply_count");
        RequiredMember<BuffPrefix> prefix = new("prefix");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("PrefixedBuff"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(duration.Name))
            {
                duration = duration.From(member.Value);
            }
            else if (member.NameEquals(status.Name))
            {
                status = status.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(applyCount.Name))
            {
                applyCount = applyCount.From(member.Value);
            }
            else if (member.NameEquals(prefix.Name))
            {
                prefix = prefix.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PrefixedBuffSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.GetValueOrEmpty(),
            Description = description.GetValueOrEmpty(),
            ApplyCount = applyCount.GetValue(),
            Prefix = prefix.Select(value => ReadBuffPrefix(value, missingMemberBehavior))
        };
    }

    private static BuffPrefix ReadBuffPrefix(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        OptionalMember<string> status = new("status");
        OptionalMember<string> description = new("description");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(status.Name))
            {
                status = status.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuffPrefix
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Status = status.GetValueOrEmpty(),
            Description = description.GetValueOrEmpty()
        };
    }

    private static PercentSkillFact ReadPercentSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<double> percent = new("percent");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Percent"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(percent.Name) || member.NameEquals("value"))
            {
                // Some use the name 'percent', some use 'value'... weird
                percent = percent.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PercentSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Percent = percent.GetValue()
        };
    }

    private static NumberSkillFact ReadNumberSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> value = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Number"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(value.Name))
            {
                value = value.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new NumberSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Value = value.GetValue()
        };
    }

    private static NoDataSkillFact ReadNoDataSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("NoData"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new NoDataSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue()
        };
    }

    private static HealingAdjustSkillFact ReadHealingAdjustSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> hitCount = new("hit_count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("HealingAdjust"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(hitCount.Name))
            {
                hitCount = hitCount.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HealingAdjustSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            HitCount = hitCount.GetValue()
        };
    }

    private static DurationSkillFact ReadDurationSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<TimeSpan> duration = new("duration");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Duration"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(duration.Name))
            {
                duration = duration.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DurationSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble()))
        };
    }

    private static DistanceSkillFact ReadDistanceSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> distance = new("distance");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Distance"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(distance.Name))
            {
                distance = distance.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DistanceSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Distance = distance.GetValue()
        };
    }

    private static DamageSkillFact ReadDamageSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> hitCount = new("hit_count");
        RequiredMember<double> damageMultiplier = new("dmg_multiplier");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Damage"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(hitCount.Name))
            {
                hitCount = hitCount.From(member.Value);
            }
            else if (member.NameEquals(damageMultiplier.Name))
            {
                damageMultiplier = damageMultiplier.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DamageSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            HitCount = hitCount.GetValue(),
            DamageMultiplier = damageMultiplier.GetValue()
        };
    }

    private static SkillFact ReadBuffSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        NullableMember<TimeSpan> duration = new("duration");
        OptionalMember<string> status = new("status");
        OptionalMember<string> description = new("description");
        NullableMember<int> applyCount = new("apply_count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Buff"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(duration.Name))
            {
                duration = duration.From(member.Value);
            }
            else if (member.NameEquals(status.Name))
            {
                status = status.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(applyCount.Name))
            {
                applyCount = applyCount.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuffSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.GetValueOrEmpty(),
            Description = description.GetValueOrEmpty(),
            ApplyCount = applyCount.GetValue()
        };
    }

    private static AttributeAdjustSkillFact ReadAttributeAdjustSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        OptionalMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        NullableMember<int> value = new("value");
        RequiredMember<AttributeAdjustTarget> target = new("target");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("AttributeAdjust"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(value.Name))
            {
                value = value.From(member.Value);
            }
            else if (member.NameEquals(target.Name))
            {
                target = target.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AttributeAdjustSkillFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValue(),
            Value = value.GetValue(),
            Target = target.GetValue(missingMemberBehavior)
        };
    }

    private static ComboFieldSkillFact ReadComboFieldSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<ComboFieldName> fieldType = new("field_type");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("ComboField"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(fieldType.Name))
            {
                fieldType = fieldType.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ComboFieldSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Field = fieldType.GetValue(missingMemberBehavior)
        };
    }

    private static ComboFinisherSkillFact ReadComboFinisherSkillFact(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> percent = new("percent");
        RequiredMember<ComboFinisherName> finisherType = new("finisher_type");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("ComboFinisher"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                        );
                }
            }
            else if (member.NameEquals("chance") && IsDefaultInt32(member))
            {
                // Ignore zero values, looks like unsanitized data
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = text.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(percent.Name))
            {
                percent = percent.From(member.Value);
            }
            else if (member.NameEquals(finisherType.Name))
            {
                finisherType = finisherType.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ComboFinisherSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Percent = percent.GetValue(),
            FinisherName = finisherType.GetValue(missingMemberBehavior)
        };

        static bool IsDefaultInt32(JsonProperty jsonProperty)
        {
            return jsonProperty.Value.ValueKind == JsonValueKind.Number
                && jsonProperty.Value.TryGetInt32(out var value)
                && value == 0;
        }
    }

    private static SkillReference ReadSubskill(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        NullableMember<Attunement> attunement = new("attunement");
        NullableMember<Transformation> form = new("form");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(attunement.Name))
            {
                attunement = attunement.From(member.Value);
            }
            else if (member.NameEquals(form.Name))
            {
                form = form.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillReference
        {
            Id = id.GetValue(),
            Attunement = attunement.GetValue(missingMemberBehavior),
            Form = form.GetValue(missingMemberBehavior)
        };
    }
}
