using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    public sealed class SkillReader : ISkillReader,
        IJsonReader<BundleSkill>,
        IJsonReader<EliteSkill>,
        IJsonReader<HealSkill>,
        IJsonReader<MonsterSkill>,
        IJsonReader<PetSkill>,
        IJsonReader<ProfessionSkill>,
        IJsonReader<ToolbeltSkill>,
        IJsonReader<TransformSkill>,
        IJsonReader<UtilitySkill>,
        IJsonReader<WeaponSkill>
    {
        BundleSkill IJsonReader<BundleSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Bundle"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior)
            };
        }

        EliteSkill IJsonReader<EliteSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var transformSkills = new OptionalMember<int[]>("transform_skills");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var specialization = new NullableMember<int>("specialization");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");
            var subskills = new OptionalMember<SkillReference[]>("subskills");
            var bundleSkills = new OptionalMember<int[]>("bundle_skills");
            var attunement = new NullableMember<Attunement>("attunement");
            var cost = new NullableMember<int>("cost");
            var toolbeltSkill = new NullableMember<int>("toolbelt_skill");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Elite"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                TransformSkills = transformSkills.Select(value => value.GetArray(item => item.GetInt32())),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                Specialization = specialization.GetValue(),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior),
                Subskills =
                    subskills.Select(value => value.GetArray(item => ReadSubskill(item, missingMemberBehavior))),
                BundleSkills = bundleSkills.Select(value => value.GetArray(item => item.GetInt32())),
                Attunement = attunement.GetValue(missingMemberBehavior),
                Cost = cost.GetValue(),
                ToolbeltSkill = toolbeltSkill.GetValue()
            };
        }

        HealSkill IJsonReader<HealSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var specialization = new NullableMember<int>("specialization");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");
            var subskills = new OptionalMember<SkillReference[]>("subskills");
            var bundleSkills = new OptionalMember<int[]>("bundle_skills");
            var attunement = new OptionalMember<Attunement>("attunement");
            var cost = new NullableMember<int>("cost");
            var toolbeltSkill = new NullableMember<int>("toolbelt_skill");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Heal"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                Specialization = specialization.GetValue(),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior),
                Subskills =
                    subskills.Select(value => value.GetArray(item => ReadSubskill(item, missingMemberBehavior))),
                BundleSkills = bundleSkills.Select(value => value.GetArray(item => item.GetInt32())),
                Attunement = attunement.GetValue(missingMemberBehavior),
                Cost = cost.GetValue(),
                ToolbeltSkill = toolbeltSkill.GetValue()
            };
        }

        MonsterSkill IJsonReader<MonsterSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Monster"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior)
            };
        }

        PetSkill IJsonReader<PetSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Pet"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior)
            };
        }

        ProfessionSkill IJsonReader<ProfessionSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var transformSkills = new OptionalMember<int[]>("transform_skills");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var specialization = new NullableMember<int>("specialization");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");
            var attunement = new NullableMember<Attunement>("attunement");
            var cost = new NullableMember<int>("cost");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Profession"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                TransformSkills = transformSkills.Select(value => value.GetArray(item => item.GetInt32())),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                Specialization = specialization.GetValue(),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior),
                Attunement = attunement.GetValue(missingMemberBehavior),
                Cost = cost.GetValue()
            };
        }

        ToolbeltSkill IJsonReader<ToolbeltSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Toolbelt"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior)
            };
        }

        TransformSkill IJsonReader<TransformSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Transform"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior)
            };
        }

        UtilitySkill IJsonReader<UtilitySkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var specialization = new NullableMember<int>("specialization");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");
            var subskills = new OptionalMember<SkillReference[]>("subskills");
            var bundleSkills = new OptionalMember<int[]>("bundle_skills");
            var attunement = new NullableMember<Attunement>("attunement");
            var toolbeltSkill = new NullableMember<int>("toolbelt_skill");
            var cost = new NullableMember<int>("cost");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Utility"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                Specialization = specialization.GetValue(),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior),
                Subskills =
                    subskills.Select(value => value.GetArray(item => ReadSubskill(item, missingMemberBehavior))),
                BundleSkills = bundleSkills.Select(value => value.GetArray(item => item.GetInt32())),
                Attunement = attunement.GetValue(missingMemberBehavior),
                ToolbeltSkill = toolbeltSkill.GetValue(),
                Cost = cost.GetValue()
            };
        }

        WeaponSkill IJsonReader<WeaponSkill>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var chatLink = new RequiredMember<string>("chat_link");
            var attunement = new NullableMember<Attunement>("attunement");
            var dualAttunement = new NullableMember<Attunement>("dual_attunement");
            var specialization = new NullableMember<int>("specialization");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");
            var cost = new NullableMember<int>("cost");
            var offhand = new NullableMember<Offhand>("dual_wield");
            var initiative = new NullableMember<int>("initiative");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Weapon"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Specialization = specialization.GetValue(),
                Attunement = attunement.GetValue(missingMemberBehavior),
                DualAttunement = dualAttunement.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior),
                Cost = cost.GetValue(),
                Offhand = offhand.GetValue(missingMemberBehavior),
                Initiative = initiative.GetValue()
            };
        }

        public Skill Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            // Unlike most models with a 'type' property, skills don't have always have it
            if (json.TryGetProperty("type", out var type))
            {
                switch (type.GetString())
                {
                    case "Bundle":
                        return ((IJsonReader<BundleSkill>) this).Read(json, missingMemberBehavior);
                    case "Elite":
                        return ((IJsonReader<EliteSkill>) this).Read(json, missingMemberBehavior);
                    case "Heal":
                        return ((IJsonReader<HealSkill>) this).Read(json, missingMemberBehavior);
                    case "Monster":
                        return ((IJsonReader<MonsterSkill>) this).Read(json, missingMemberBehavior);
                    case "Pet":
                        return ((IJsonReader<PetSkill>) this).Read(json, missingMemberBehavior);
                    case "Profession":
                        return ((IJsonReader<ProfessionSkill>) this).Read(json, missingMemberBehavior);
                    case "Toolbelt":
                        return ((IJsonReader<ToolbeltSkill>) this).Read(json, missingMemberBehavior);
                    case "Transform":
                        return ((IJsonReader<TransformSkill>)this).Read(json, missingMemberBehavior);
                    case "Utility":
                        return ((IJsonReader<UtilitySkill>) this).Read(json, missingMemberBehavior);
                    case "Weapon":
                        return ((IJsonReader<WeaponSkill>) this).Read(json, missingMemberBehavior);
                }
            }

            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var facts = new OptionalMember<SkillFact[]>("facts");
            var traitedFacts = new OptionalMember<TraitedSkillFact[]>("traited_facts");
            var description = new RequiredMember<string>("description");
            var icon = new OptionalMember<string>("icon");
            var weaponType = new NullableMember<WeaponType>("weapon_type");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var slot = new NullableMember<SkillSlot>("slot");
            var flipSkill = new NullableMember<int>("flip_skill");
            var nextChain = new NullableMember<int>("next_chain");
            var prevChain = new NullableMember<int>("prev_chain");
            var flags = new RequiredMember<SkillFlag[]>("flags");
            var chatLink = new RequiredMember<string>("chat_link");
            var categories = new OptionalMember<SkillCategoryName[]>("categories");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
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
                    facts.Select(value =>
                        value.GetArray(item => ReadSkillFact(item, missingMemberBehavior, out _, out _))),
                TraitedFacts =
                    traitedFacts.Select(value =>
                        value.GetArray(item => ReadTraitedSkillFact(item, missingMemberBehavior))),
                Description = description.GetValue(),
                Icon = icon.GetValueOrNull(),
                WeaponType = weaponType.GetValue(missingMemberBehavior),
                Professions = professions.GetValue(missingMemberBehavior),
                Slot = slot.GetValue(missingMemberBehavior),
                FlipSkill = flipSkill.GetValue(),
                NextChain = nextChain.GetValue(),
                PreviousChain = prevChain.GetValue(),
                SkillFlag = flags.GetValue(missingMemberBehavior),
                ChatLink = chatLink.GetValue(),
                Categories = categories.GetValue(missingMemberBehavior)
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        private TraitedSkillFact ReadTraitedSkillFact(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var fact = ReadSkillFact(json, missingMemberBehavior, out var requiresTrait, out var overrides);
            return new TraitedSkillFact
            {
                Fact = fact,
                RequiresTrait = requiresTrait.GetValueOrDefault(),
                Overrides = overrides
            };
        }

        private SkillFact ReadSkillFact(
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
                return ReadPercentSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
            }

            switch (type.GetString())
            {
                case "AttributeAdjust":
                    return ReadAttributeAdjustSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "ComboField":
                    return ReadComboFieldSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "ComboFinisher":
                    return ReadComboFinisherSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Buff":
                    return ReadBuffSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Damage":
                    return ReadDamageSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Distance":
                    return ReadDistanceSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Duration":
                    return ReadDurationSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "HealingAdjust":
                    return ReadHealingAdjustSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "NoData":
                    return ReadNoDataSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Number":
                    return ReadNumberSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Percent":
                    return ReadPercentSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "PrefixedBuff":
                    return ReadPrefixedBuffSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Radius":
                    return ReadRadiusSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Range":
                    return ReadRangeSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Recharge":
                    return ReadRechargeSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "StunBreak":
                    return ReadStunBreakSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Time":
                    return ReadTimeSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
                case "Unblockable":
                    return ReadUnblockableSkillFact(json, missingMemberBehavior, out requiresTrait, out overrides);
            }

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
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

        private RadiusSkillFact ReadRadiusSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var distance = new RequiredMember<int>("distance");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Radius"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private RangeSkillFact ReadRangeSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var value = new RequiredMember<int>("value");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Range"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private RechargeSkillFact ReadRechargeSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var value = new RequiredMember<double>("value");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Recharge"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private StunBreakSkillFact ReadStunBreakSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var value = new RequiredMember<bool>("value");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("StunBreak"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private TimeSkillFact ReadTimeSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var duration = new RequiredMember<TimeSpan>("duration");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Time"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private UnblockableSkillFact ReadUnblockableSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var value = new RequiredMember<bool>("value");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Unblockable"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private PrefixedBuffSkillFact ReadPrefixedBuffSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var duration = new NullableMember<TimeSpan>("duration");
            var status = new OptionalMember<string>("status");
            var description = new OptionalMember<string>("description");
            var applyCount = new NullableMember<int>("apply_count");
            var prefix = new RequiredMember<BuffPrefix>("prefix");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("PrefixedBuff"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private BuffPrefix ReadBuffPrefix(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var status = new OptionalMember<string>("status");
            var description = new OptionalMember<string>("description");
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

        private PercentSkillFact ReadPercentSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var percent = new RequiredMember<double>("percent");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Percent"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private NumberSkillFact ReadNumberSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var value = new RequiredMember<int>("value");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Number"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private NoDataSkillFact ReadNoDataSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("NoData"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private HealingAdjustSkillFact ReadHealingAdjustSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var hitCount = new RequiredMember<int>("hit_count");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("HealingAdjust"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private DurationSkillFact ReadDurationSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var duration = new RequiredMember<TimeSpan>("duration");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Duration"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private DistanceSkillFact ReadDistanceSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var distance = new RequiredMember<int>("distance");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Distance"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private DamageSkillFact ReadDamageSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var hitCount = new RequiredMember<int>("hit_count");
            var damageMultiplier = new RequiredMember<double>("dmg_multiplier");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Damage"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private SkillFact ReadBuffSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var duration = new NullableMember<TimeSpan>("duration");
            var status = new OptionalMember<string>("status");
            var description = new OptionalMember<string>("description");
            var applyCount = new NullableMember<int>("apply_count");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Buff"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private AttributeAdjustSkillFact ReadAttributeAdjustSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new OptionalMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var value = new NullableMember<int>("value");
            var target = new RequiredMember<AttributeAdjustTarget>("target");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("AttributeAdjust"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private ComboFieldSkillFact ReadComboFieldSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var fieldType = new RequiredMember<ComboFieldName>("field_type");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("ComboField"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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

        private ComboFinisherSkillFact ReadComboFinisherSkillFact(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior,
            out int? requiresTrait,
            out int? overrides
        )
        {
            requiresTrait = null;
            overrides = null;

            var text = new RequiredMember<string>("text");
            var icon = new RequiredMember<string>("icon");
            var percent = new RequiredMember<int>("percent");
            var finisherType = new RequiredMember<ComboFinisherName>("finisher_type");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("ComboFinisher"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
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
        }

        private SkillReference ReadSubskill(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var attunement = new NullableMember<Attunement>("attunement");
            var form = new NullableMember<Transformation>("form");

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
}
