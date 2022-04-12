using System;
using System.Text.Json;

using GW2SDK.Json;

using JetBrains.Annotations;

namespace GW2SDK.Accounts.Json;

[PublicAPI]
public static class CharacterReader
{
    public static Character Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<Race> race = new("race");
        RequiredMember<Gender> gender = new("gender");
        RequiredMember<CharacterFlag> flags = new("flags");
        RequiredMember<ProfessionName> profession = new("profession");
        RequiredMember<int> level = new("level");
        OptionalMember<string> guild = new("guild");
        RequiredMember<TimeSpan> age = new("age");
        RequiredMember<DateTimeOffset> lastModified = new("last_modified");
        RequiredMember<DateTimeOffset> created = new("created");
        RequiredMember<int> deaths = new("deaths");
        RequiredMember<CraftingDiscipline> crafting = new("crafting");
        NullableMember<int> title = new("title");
        RequiredMember<string> backstory = new("backstory");
        OptionalMember<WvwAbility> wvwAbilities = new("wvw_abilities");
        NullableMember<int> buildTabsUnlocked = new("build_tabs_unlocked");
        NullableMember<int> activeBuildTab = new("active_build_tab");
        OptionalMember<BuildTab> buildTabs = new("build_tabs");
        NullableMember<int> equipmentTabsUnlocked = new("equipment_tabs_unlocked");
        NullableMember<int> activeEquipmentTab = new("active_equipment_tab");
        OptionalMember<EquipmentItem> equipment = new("equipment");
        OptionalMember<EquipmentTab> equipmentTabs = new("equipment_tabs");
        OptionalMember<int> recipes = new("recipes");
        OptionalMember<TrainingTrack> training = new("training");
        OptionalMember<Bag?> bags = new("bags");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(race.Name))
            {
                race = race.From(member.Value);
            }
            else if (member.NameEquals(gender.Name))
            {
                gender = gender.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = profession.From(member.Value);
            }
            else if (member.NameEquals(level.Name))
            {
                level = level.From(member.Value);
            }
            else if (member.NameEquals(guild.Name))
            {
                guild = guild.From(member.Value);
            }
            else if (member.NameEquals(age.Name))
            {
                age = age.From(member.Value);
            }
            else if (member.NameEquals(lastModified.Name))
            {
                lastModified = lastModified.From(member.Value);
            }
            else if (member.NameEquals(created.Name))
            {
                created = created.From(member.Value);
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths = deaths.From(member.Value);
            }
            else if (member.NameEquals(crafting.Name))
            {
                crafting = crafting.From(member.Value);
            }
            else if (member.NameEquals(title.Name))
            {
                title = title.From(member.Value);
            }
            else if (member.NameEquals(backstory.Name))
            {
                backstory = backstory.From(member.Value);
            }
            else if (member.NameEquals(wvwAbilities.Name))
            {
                wvwAbilities = wvwAbilities.From(member.Value);
            }
            else if (member.NameEquals(buildTabsUnlocked.Name))
            {
                buildTabsUnlocked = buildTabsUnlocked.From(member.Value);
            }
            else if (member.NameEquals(activeBuildTab.Name))
            {
                activeBuildTab = activeBuildTab.From(member.Value);
            }
            else if (member.NameEquals(buildTabs.Name))
            {
                buildTabs = buildTabs.From(member.Value);
            }
            else if (member.NameEquals(equipmentTabsUnlocked.Name))
            {
                equipmentTabsUnlocked = equipmentTabsUnlocked.From(member.Value);
            }
            else if (member.NameEquals(activeEquipmentTab.Name))
            {
                activeEquipmentTab = activeEquipmentTab.From(member.Value);
            }
            else if (member.NameEquals(equipment.Name))
            {
                equipment = equipment.From(member.Value);
            }
            else if (member.NameEquals(equipmentTabs.Name))
            {
                equipmentTabs = equipmentTabs.From(member.Value);
            }
            else if (member.NameEquals(recipes.Name))
            {
                recipes = recipes.From(member.Value);
            }
            else if (member.NameEquals(training.Name))
            {
                training = training.From(member.Value);
            }
            else if (member.NameEquals(bags.Name))
            {
                bags = bags.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Character
        {
            Name = name.GetValue(),
            Race = race.GetValue(missingMemberBehavior),
            Gender = gender.GetValue(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Level = level.GetValue(),
            GuildId = guild.GetValueOrEmpty(),
            Profession = profession.GetValue(missingMemberBehavior),
            Age = age.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.GetValue(),
            Created = created.GetValue(),
            Deaths = deaths.GetValue(),
            CraftingDisciplines = crafting.SelectMany(value => ReadCraftingDiscipline(value, missingMemberBehavior)),
            TitleId = title.GetValue(),
            Backstory = backstory.SelectMany(value => value.GetStringRequired()),
            WvwAbilities = wvwAbilities.SelectMany(value => ReadWvwAbility(value, missingMemberBehavior)),
            BuildTabsUnlocked = buildTabsUnlocked.GetValue(),
            ActiveBuildTab = activeBuildTab.GetValue(),
            BuildTabs = buildTabs.SelectMany(value => ReadBuildTab(value, missingMemberBehavior)),
            EquipmentTabsUnlocked = equipmentTabsUnlocked.GetValue(),
            ActiveEquipmentTab = activeEquipmentTab.GetValue(),
            Equipment = equipment.SelectMany(value => ReadEquipmentItem(value, missingMemberBehavior)),
            EquipmentTabs = equipmentTabs.SelectMany(value => ReadEquipmentTab(value, missingMemberBehavior)),
            Recipes = recipes.SelectMany(value => value.GetInt32()),
            Training = training.SelectMany(value => ReadTrainingObjective(value, missingMemberBehavior)),
            Bags = bags.SelectMany(value => ReadBag(value, missingMemberBehavior))
        };
    }

    private static Bag? ReadBag(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember<int> id = new("id");
        RequiredMember<int> size = new("size");
        RequiredMember<InventorySlot?> inventory = new("inventory");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(size.Name))
            {
                size = size.From(member.Value);
            }
            else if (member.NameEquals(inventory.Name))
            {
                inventory = inventory.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Bag
        {
            Id = id.GetValue(),
            Size = size.GetValue(),
            Inventory = inventory.SelectMany(value => ReadInventorySlot(value, missingMemberBehavior))
        };
    }

    private static InventorySlot? ReadInventorySlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember<int> id = new("id");
        RequiredMember<int> count = new("count");
        NullableMember<int> charges = new("charges");
        NullableMember<int> skin = new("skin");
        OptionalMember<int> upgrades = new("upgrades");
        OptionalMember<int> upgradeSlotIndices = new("upgrade_slot_indices");
        OptionalMember<int> infusions = new("infusions");
        OptionalMember<int?> dyes = new("dyes");
        OptionalMember<ItemBinding> binding = new("binding");
        OptionalMember<string> boundTo = new("bound_to");
        OptionalMember<SelectedStat> stats = new("stats");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(count.Name))
            {
                count = count.From(member.Value);
            }
            else if (member.NameEquals(charges.Name))
            {
                charges = charges.From(member.Value);
            }
            else if (member.NameEquals(skin.Name))
            {
                skin = skin.From(member.Value);
            }
            else if (member.NameEquals(upgrades.Name))
            {
                upgrades = upgrades.From(member.Value);
            }
            else if (member.NameEquals(upgradeSlotIndices.Name))
            {
                upgradeSlotIndices = upgradeSlotIndices.From(member.Value);
            }
            else if (member.NameEquals(infusions.Name))
            {
                infusions = infusions.From(member.Value);
            }
            else if (member.NameEquals(dyes.Name))
            {
                dyes = dyes.From(member.Value);
            }
            else if (member.NameEquals(binding.Name))
            {
                binding = binding.From(member.Value);
            }
            else if (member.NameEquals(boundTo.Name))
            {
                boundTo = boundTo.From(member.Value);
            }
            else if (member.NameEquals(stats.Name))
            {
                stats = stats.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InventorySlot
        {
            Id = id.GetValue(),
            Count = count.GetValue(),
            Charges = charges.GetValue(),
            Upgrades = upgrades.SelectMany(value => value.GetInt32()),
            UpgradeSlotIndices = upgradeSlotIndices.SelectMany(value => value.GetInt32()),
            Infusions = infusions.SelectMany(value => value.GetInt32()),
            Dyes = dyes.SelectMany(value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()),
            Binding = binding.GetValue(missingMemberBehavior),
            BoundTo = boundTo.GetValueOrEmpty(),
            Stats = stats.Select(value => ReadSelectedStat(value, missingMemberBehavior))
        };
    }

    private static EquipmentItem ReadEquipmentItem(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        NullableMember<int> count = new("count");
        NullableMember<EquipmentSlot> slot = new("slot");
        OptionalMember<int> upgrades = new("upgrades");
        OptionalMember<int> infusions = new("infusions");
        NullableMember<int> skin = new("skin");
        OptionalMember<SelectedStat> stats = new("stats");
        OptionalMember<ItemBinding> binding = new("binding");
        OptionalMember<string> boundTo = new("bound_to");
        RequiredMember<EquipmentLocation> location = new("location");
        OptionalMember<int> tabs = new("tabs");
        OptionalMember<int?> dyes = new("dyes");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(count.Name))
            {
                count = count.From(member.Value);
            }
            else if (member.NameEquals(slot.Name))
            {
                slot = slot.From(member.Value);
            }
            else if (member.NameEquals(upgrades.Name))
            {
                upgrades = upgrades.From(member.Value);
            }
            else if (member.NameEquals(infusions.Name))
            {
                infusions = infusions.From(member.Value);
            }
            else if (member.NameEquals(skin.Name))
            {
                skin = skin.From(member.Value);
            }
            else if (member.NameEquals(stats.Name))
            {
                stats = stats.From(member.Value);
            }
            else if (member.NameEquals(binding.Name))
            {
                binding = binding.From(member.Value);
            }
            else if (member.NameEquals(boundTo.Name))
            {
                boundTo = boundTo.From(member.Value);
            }
            else if (member.NameEquals(location.Name))
            {
                location = location.From(member.Value);
            }
            else if (member.NameEquals(tabs.Name))
            {
                tabs = tabs.From(member.Value);
            }
            else if (member.NameEquals(dyes.Name))
            {
                dyes = dyes.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EquipmentItem
        {
            Id = id.GetValue(),
            Count = count.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior),
            Upgrades = upgrades.SelectMany(value => value.GetInt32()),
            Infusions = infusions.SelectMany(value => value.GetInt32()),
            SkinId = skin.GetValue(),
            Stats = stats.Select(value => ReadSelectedStat(value, missingMemberBehavior)),
            Binding = binding.GetValue(missingMemberBehavior),
            BoundTo = boundTo.GetValueOrEmpty(),
            Location = location.GetValue(missingMemberBehavior),
            Tabs = tabs.SelectMany(value => value.GetInt32()),
            Dyes = dyes.SelectMany(value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32())
        };
    }

    private static SelectedStat ReadSelectedStat(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SelectedModification> attributes = new("attributes");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(attributes.Name))
            {
                attributes = attributes.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedStat
        {
            Id = id.GetValue(),
            Attributes = attributes.Select(value => ReadSelectedModification(value, missingMemberBehavior))
        };
    }

    private static SelectedModification ReadSelectedModification(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember<int> agonyResistance = new("AgonyResistance");
        NullableMember<int> boonDuration = new("BoonDuration");
        NullableMember<int> conditionDamage = new("ConditionDamage");
        NullableMember<int> conditionDuration = new("ConditionDuration");
        NullableMember<int> critDamage = new("CritDamage");
        NullableMember<int> healing = new("Healing");
        NullableMember<int> power = new("Power");
        NullableMember<int> precision = new("Precision");
        NullableMember<int> toughness = new("Toughness");
        NullableMember<int> vitality = new("Vitality");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(agonyResistance.Name))
            {
                agonyResistance = agonyResistance.From(member.Value);
            }
            else if (member.NameEquals(boonDuration.Name))
            {
                boonDuration = boonDuration.From(member.Value);
            }
            else if (member.NameEquals(conditionDamage.Name))
            {
                conditionDamage = conditionDamage.From(member.Value);
            }
            else if (member.NameEquals(conditionDuration.Name))
            {
                conditionDuration = conditionDuration.From(member.Value);
            }
            else if (member.NameEquals(critDamage.Name))
            {
                critDamage = critDamage.From(member.Value);
            }
            else if (member.NameEquals(healing.Name))
            {
                healing = healing.From(member.Value);
            }
            else if (member.NameEquals(power.Name))
            {
                power = power.From(member.Value);
            }
            else if (member.NameEquals(precision.Name))
            {
                precision = precision.From(member.Value);
            }
            else if (member.NameEquals(toughness.Name))
            {
                toughness = toughness.From(member.Value);
            }
            else if (member.NameEquals(vitality.Name))
            {
                vitality = vitality.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedModification
        {
            AgonyResistance = agonyResistance.GetValue(),
            BoonDuration = boonDuration.GetValue(),
            ConditionDamage = conditionDamage.GetValue(),
            ConditionDuration = conditionDuration.GetValue(),
            CritDamage = critDamage.GetValue(),
            Healing = healing.GetValue(),
            Power = power.GetValue(),
            Precision = precision.GetValue(),
            Toughness = toughness.GetValue(),
            Vitality = vitality.GetValue()
        };
    }

    private static TrainingTrack ReadTrainingObjective(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> spent = new("spent");
        RequiredMember<bool> done = new("done");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(spent.Name))
            {
                spent = spent.From(member.Value);
            }
            else if (member.NameEquals(done.Name))
            {
                done = done.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TrainingTrack
        {
            Id = id.GetValue(),
            Spent = spent.GetValue(),
            Done = done.GetValue()
        };
    }

    private static EquipmentTab ReadEquipmentTab(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> tab = new("tab");
        RequiredMember<string> name = new("name");
        RequiredMember<EquipmentItem> equipment = new("equipment");
        RequiredMember<PvpEquipment> pvpEquipment = new("equipment_pvp");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(tab.Name))
            {
                tab = tab.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals("is_active"))
            {
                // Ignore this because you should only use ActiveEquipmentTab
                // => player.EquipmentTabs.Single(tab => tab.Tab == player.ActiveEquipmentTab);

                // Otherwise you have to update two objects when the active tab changes and you can't do that atomically
            }
            else if (member.NameEquals(equipment.Name))
            {
                equipment = equipment.From(member.Value);
            }
            else if (member.NameEquals(pvpEquipment.Name))
            {
                pvpEquipment = pvpEquipment.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EquipmentTab
        {
            Tab = tab.GetValue(),
            Name = name.GetValue(),
            Equipment = equipment.SelectMany(value => ReadEquipmentItem(value, missingMemberBehavior)),
            PvpEquipment = pvpEquipment.Select(value => ReadPvpEquipment(value, missingMemberBehavior))
        };
    }

    private static PvpEquipment ReadPvpEquipment(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        NullableMember<int> amulet = new("amulet");
        NullableMember<int> rune = new("rune");
        RequiredMember<int?> sigils = new("sigils");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(amulet.Name))
            {
                amulet = amulet.From(member.Value);
            }
            else if (member.NameEquals(rune.Name))
            {
                rune = rune.From(member.Value);
            }
            else if (member.NameEquals(sigils.Name))
            {
                sigils = sigils.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PvpEquipment
        {
            AmuletId = amulet.GetValue(),
            RuneId = rune.GetValue(),
            SigilIds = sigils.SelectMany(value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32())
        };
    }

    private static BuildTab ReadBuildTab(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> tab = new("tab");
        RequiredMember<Build> build = new("build");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(tab.Name))
            {
                tab = tab.From(member.Value);
            }
            else if (member.NameEquals(build.Name))
            {
                build = build.From(member.Value);
            }
            else if (member.NameEquals("is_active"))
            {
                // Ignore this because you should only use ActiveBuildTab
                // => player.BuildTabs.Single(tab => tab.Tab == player.ActiveBuildTab);

                // Otherwise you have to update two objects when the active tab changes and you can't do that atomically
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuildTab
        {
            Tab = tab.GetValue(),
            Build = build.Select(value => ReadBuild(value, missingMemberBehavior))
        };
    }

    private static Build ReadBuild(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<ProfessionName> profession = new("profession");
        RequiredMember<Specialization> specializations = new("specializations");
        RequiredMember<SkillBar> skills = new("skills");
        RequiredMember<SkillBar> aquaticSkills = new("aquatic_skills");
        OptionalMember<PetSkillBar> pets = new("pets");
        OptionalMember<string?> legends = new("legends");
        OptionalMember<string?> aquaticLegends = new("aquatic_legends");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = profession.From(member.Value);
            }
            else if (member.NameEquals(specializations.Name))
            {
                specializations = specializations.From(member.Value);
            }
            else if (member.NameEquals(skills.Name))
            {
                skills = skills.From(member.Value);
            }
            else if (member.NameEquals(aquaticSkills.Name))
            {
                aquaticSkills = aquaticSkills.From(member.Value);
            }
            else if (member.NameEquals(pets.Name))
            {
                pets = pets.From(member.Value);
            }
            else if (member.NameEquals(legends.Name))
            {
                legends = legends.From(member.Value);
            }
            else if (member.NameEquals(aquaticLegends.Name))
            {
                aquaticLegends = aquaticLegends.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Build
        {
            Name = name.GetValue(),
            Profession = profession.GetValue(missingMemberBehavior),
            Specializations = specializations.SelectMany(value => ReadSpecialization(value, missingMemberBehavior)),
            Skills = skills.Select(value => ReadSkillBar(value, missingMemberBehavior)),
            AquaticSkills = aquaticSkills.Select(value => ReadSkillBar(value, missingMemberBehavior)),
            Pets = pets.Select(value => ReadPets(value, missingMemberBehavior)),
            Legends = legends.SelectMany(value => value.GetString()),
            AquaticLegends = aquaticLegends.SelectMany(value => value.GetString())
        };
    }

    private static SkillBar ReadSkillBar(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        NullableMember<int> heal = new("heal");
        RequiredMember<int?> utilities = new("utilities");
        NullableMember<int> elite = new("elite");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(heal.Name))
            {
                heal = heal.From(member.Value);
            }
            else if (member.NameEquals(utilities.Name))
            {
                utilities = utilities.From(member.Value);
            }
            else if (member.NameEquals(elite.Name))
            {
                elite = elite.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillBar
        {
            Heal = heal.GetValue(),
            Utilities = utilities.SelectMany(value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()),
            Elite = elite.GetValue()
        };
    }

    private static Specialization ReadSpecialization(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        NullableMember<int> id = new("id");
        RequiredMember<int?> traits = new("traits");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(traits.Name))
            {
                traits = traits.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Specialization
        {
            Id = id.GetValue(),
            Traits = traits.SelectMany(value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32())
        };
    }

    private static PetSkillBar ReadPets(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int?> terrestrial = new("terrestrial");
        RequiredMember<int?> aquatic = new("aquatic");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(terrestrial.Name))
            {
                terrestrial = terrestrial.From(member.Value);
            }
            else if (member.NameEquals(aquatic.Name))
            {
                aquatic = aquatic.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PetSkillBar
        {
            Terrestrial =
                terrestrial.SelectMany(value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()),
            Aquatic = aquatic.SelectMany(value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32())
        };
    }

    private static WvwAbility ReadWvwAbility(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> rank = new("rank");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(rank.Name))
            {
                rank = rank.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WvwAbility
        {
            Id = id.GetValue(),
            Rank = rank.GetValue()
        };
    }

    private static CraftingDiscipline ReadCraftingDiscipline(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<CraftingDisciplineName> discipline = new("discipline");
        RequiredMember<int> rating = new("rating");
        RequiredMember<bool> active = new("active");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(discipline.Name))
            {
                discipline = discipline.From(member.Value);
            }
            else if (member.NameEquals(rating.Name))
            {
                rating = rating.From(member.Value);
            }
            else if (member.NameEquals(active.Name))
            {
                active = active.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CraftingDiscipline
        {
            Discipline = discipline.GetValue(missingMemberBehavior),
            Rating = rating.GetValue(),
            Active = active.GetValue()
        };
    }
}