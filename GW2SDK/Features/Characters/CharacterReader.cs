using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    public sealed class CharacterReader : ICharacterReader, IJsonReader<UnlockedRecipesView>
    {
        public Character Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var race = new RequiredMember<Race>("race");
            var gender = new RequiredMember<Gender>("gender");
            var flags = new RequiredMember<CharacterFlag[]>("flags");
            var profession = new RequiredMember<ProfessionName>("profession");
            var level = new RequiredMember<int>("level");
            var guild = new OptionalMember<string>("guild");
            var age = new RequiredMember<TimeSpan>("age");
            var lastModified = new RequiredMember<DateTimeOffset>("last_modified");
            var created = new RequiredMember<DateTimeOffset>("created");
            var deaths = new RequiredMember<int>("deaths");
            var crafting = new RequiredMember<CraftingDiscipline[]>("crafting");
            var title = new NullableMember<int>("title");
            var backstory = new RequiredMember<string[]>("backstory");
            var wvwAbilities = new OptionalMember<WvwAbility[]>("wvw_abilities");
            var buildTabsUnlocked = new NullableMember<int>("build_tabs_unlocked");
            var activeBuildTab = new NullableMember<int>("active_build_tab");
            var buildTabs = new OptionalMember<BuildTab[]>("build_tabs");
            var equipmentTabsUnlocked = new NullableMember<int>("equipment_tabs_unlocked");
            var activeEquipmentTab = new NullableMember<int>("active_equipment_tab");
            var equipment = new OptionalMember<EquipmentItem[]>("equipment");
            var equipmentTabs = new OptionalMember<EquipmentTab[]>("equipment_tabs");
            var recipes = new OptionalMember<int[]>("recipes");
            var training = new OptionalMember<TrainingObjective[]>("training");
            var bags = new OptionalMember<Bag?[]>("bags");

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
                Flags = flags.GetValue(missingMemberBehavior),
                Level = level.GetValue(),
                GuildId = guild.GetValueOrEmpty(),
                Profession = profession.GetValue(missingMemberBehavior),
                Age = age.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
                LastModified = lastModified.GetValue(),
                Created = created.GetValue(),
                Deaths = deaths.GetValue(),
                CraftingDisciplines =
                    crafting.Select(value =>
                        value.GetArray(item => ReadCraftingDiscipline(item, missingMemberBehavior))),
                TitleId = title.GetValue(),
                Backstory = backstory.Select(value => value.GetArray(item => item.GetStringRequired())),
                WvwAbilities =
                    wvwAbilities.Select(value => value.GetArray(item => ReadWvwAbility(item, missingMemberBehavior))),
                BuildTabsUnlocked = buildTabsUnlocked.GetValue(),
                ActiveBuildTab = activeBuildTab.GetValue(),
                BuildTabs =
                    buildTabs.Select(value => value.GetArray(item => ReadBuildTab(item, missingMemberBehavior))),
                EquipmentTabsUnlocked = equipmentTabsUnlocked.GetValue(),
                ActiveEquipmentTab = activeEquipmentTab.GetValue(),
                Equipment =
                    equipment.Select(value => value.GetArray(item => ReadEquipmentItem(item, missingMemberBehavior))),
                EquipmentTabs =
                    equipmentTabs.Select(value =>
                        value.GetArray(item => ReadEquipmentTab(item, missingMemberBehavior))),
                Recipes = recipes.Select(value => value.GetArray(item => item.GetInt32())),
                Training =
                    training.Select(value =>
                        value.GetArray(item => ReadTrainingObjective(item, missingMemberBehavior))),
                Bags = bags.Select(value => value.GetArray(item => ReadBag(item, missingMemberBehavior)))
            };
        }

        public IJsonReader<string> Name { get; } = new StringJsonReader();

        public IJsonReader<UnlockedRecipesView> Recipes => this;

        UnlockedRecipesView IJsonReader<UnlockedRecipesView>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var recipes = new RequiredMember<int[]>("recipes");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(recipes.Name))
                {
                    recipes = recipes.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new UnlockedRecipesView
            {
                Recipes = recipes.Select(value => value.GetArray(item => item.GetInt32()))
            };
        }

        private Bag? ReadBag(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
            if (json.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            var id = new RequiredMember<int>("id");
            var size = new RequiredMember<int>("size");
            var inventory = new RequiredMember<InventorySlot?[]>("inventory");
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
                Inventory = inventory.Select(value =>
                    value.GetArray(item => ReadInventorySlot(item, missingMemberBehavior)))
            };
        }

        private InventorySlot? ReadInventorySlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
            if (json.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            var id = new RequiredMember<int>("id");
            var count = new RequiredMember<int>("count");
            var charges = new NullableMember<int>("charges");
            var skin = new NullableMember<int>("skin");
            var upgrades = new OptionalMember<int[]>("upgrades");
            var upgradeSlotIndices = new OptionalMember<int[]>("upgrade_slot_indices");
            var infusions = new OptionalMember<int[]>("infusions");
            var dyes = new OptionalMember<int?[]>("dyes");
            var binding = new OptionalMember<ItemBinding>("binding");
            var boundTo = new OptionalMember<string>("bound_to");
            var stats = new OptionalMember<SelectedStat>("stats");

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
                Upgrades = upgrades.Select(value => value.GetArray(item => item.GetInt32())),
                UpgradeSlotIndices = upgradeSlotIndices.Select(value => value.GetArray(item => item.GetInt32())),
                Infusions = infusions.Select(value => value.GetArray(item => item.GetInt32())),
                Dyes = dyes.Select(value =>
                    value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : (int?)item.GetInt32())),
                Binding = binding.GetValue(missingMemberBehavior),
                BoundTo = boundTo.GetValueOrEmpty(),
                Stats = stats.Select(value => ReadSelectedStat(value, missingMemberBehavior))
            };
        }

        private EquipmentItem ReadEquipmentItem(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var count = new NullableMember<int>("count");
            var slot = new NullableMember<EquipmentSlot>("slot");
            var upgrades = new OptionalMember<int[]>("upgrades");
            var infusions = new OptionalMember<int[]>("infusions");
            var skin = new NullableMember<int>("skin");
            var stats = new OptionalMember<SelectedStat>("stats");
            var binding = new OptionalMember<ItemBinding>("binding");
            var boundTo = new OptionalMember<string>("bound_to");
            var location = new RequiredMember<EquipmentLocation>("location");
            var tabs = new OptionalMember<int[]>("tabs");
            var dyes = new OptionalMember<int?[]>("dyes");

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
                Upgrades = upgrades.Select(value => value.GetArray(item => item.GetInt32())),
                Infusions = infusions.Select(value => value.GetArray(item => item.GetInt32())),
                SkinId = skin.GetValue(),
                Stats = stats.Select(value => ReadSelectedStat(value, missingMemberBehavior)),
                Binding = binding.GetValue(missingMemberBehavior),
                BoundTo = boundTo.GetValueOrEmpty(),
                Location = location.GetValue(missingMemberBehavior),
                Tabs = tabs.Select(value => value.GetArray(item => item.GetInt32())),
                Dyes = dyes.Select(value =>
                    value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : (int?)item.GetInt32()))
            };
        }

        private SelectedStat ReadSelectedStat(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var attributes = new RequiredMember<SelectedModification>("attributes");
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

        private SelectedModification ReadSelectedModification(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var agonyResistance = new NullableMember<int>("AgonyResistance");
            var boonDuration = new NullableMember<int>("BoonDuration");
            var conditionDamage = new NullableMember<int>("ConditionDamage");
            var conditionDuration = new NullableMember<int>("ConditionDuration");
            var critDamage = new NullableMember<int>("CritDamage");
            var healing = new NullableMember<int>("Healing");
            var power = new NullableMember<int>("Power");
            var precision = new NullableMember<int>("Precision");
            var toughness = new NullableMember<int>("Toughness");
            var vitality = new NullableMember<int>("Vitality");
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

        private TrainingObjective ReadTrainingObjective(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var spent = new RequiredMember<int>("spent");
            var done = new RequiredMember<bool>("done");

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

            return new TrainingObjective
            {
                Id = id.GetValue(),
                Spent = spent.GetValue(),
                Done = done.GetValue()
            };
        }

        private EquipmentTab ReadEquipmentTab(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var tab = new RequiredMember<int>("tab");
            var name = new RequiredMember<string>("name");
            var equipment = new RequiredMember<EquipmentItem[]>("equipment");
            var pvpEquipment = new RequiredMember<PvpEquipment>("equipment_pvp");

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
                Equipment = equipment.Select(value =>
                    value.GetArray(item => ReadEquipmentItem(item, missingMemberBehavior))),
                PvpEquipment = pvpEquipment.Select(value => ReadPvpEquipment(value, missingMemberBehavior))
            };
        }

        private PvpEquipment ReadPvpEquipment(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var amulet = new NullableMember<int>("amulet");
            var rune = new NullableMember<int>("rune");
            var sigils = new RequiredMember<int?[]>("sigils");

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
                SigilIds = sigils.Select(value =>
                    value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : (int?)item.GetInt32()))
            };
        }

        private BuildTab ReadBuildTab(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var tab = new RequiredMember<int>("tab");
            var build = new RequiredMember<Build>("build");

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

        private Build ReadBuild(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var profession = new RequiredMember<ProfessionName>("profession");
            var specializations = new RequiredMember<Specialization[]>("specializations");
            var skills = new RequiredMember<SkillBar>("skills");
            var aquaticSkills = new RequiredMember<SkillBar>("aquatic_skills");
            var pets = new OptionalMember<PetSkillBar>("pets");
            var legends = new OptionalMember<string?[]>("legends");
            var aquaticLegends = new OptionalMember<string?[]>("aquatic_legends");

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
                Specializations =
                    specializations.Select(value =>
                        value.GetArray(item => ReadSpecialization(item, missingMemberBehavior))),
                Skills = skills.Select(value => ReadSkillBar(value, missingMemberBehavior)),
                AquaticSkills = aquaticSkills.Select(value => ReadSkillBar(value, missingMemberBehavior)),
                Pets = pets.Select(value => ReadPets(value, missingMemberBehavior)),
                Legends = legends.Select(value => ReadLegends(value)),
                AquaticLegends = aquaticLegends.Select(value => ReadLegends(value))
            };
        }

        private SkillBar ReadSkillBar(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var heal = new NullableMember<int>("heal");
            var utilities = new RequiredMember<int?[]>("utilities");
            var elite = new NullableMember<int>("elite");

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
                Utilities = utilities.Select(value =>
                    value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : (int?)item.GetInt32())),
                Elite = elite.GetValue()
            };
        }

        private Specialization ReadSpecialization(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new NullableMember<int>("id");
            var traits = new RequiredMember<int?[]>("traits");

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
                Traits = traits.Select(value =>
                    value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : (int?)item.GetInt32()))
            };
        }

        private PetSkillBar ReadPets(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var terrestrial = new RequiredMember<int?[]>("terrestrial");
            var aquatic = new RequiredMember<int?[]>("aquatic");

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
                Terrestrial = terrestrial.Select(value =>
                    value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : (int?)item.GetInt32())),
                Aquatic = aquatic.Select(value =>
                    value.GetArray(item => item.ValueKind == JsonValueKind.Null ? null : (int?)item.GetInt32()))
            };
        }

        private string?[] ReadLegends(JsonElement json)
        {
            if (json.GetArrayLength() != 2)
            {
                throw new InvalidOperationException("There should be two legends.");
            }

            return new[]
            {
                json[0]
                    .GetString(),
                json[1]
                    .GetString()
            };
        }

        private WvwAbility ReadWvwAbility(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var rank = new RequiredMember<int>("rank");

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

        private CraftingDiscipline ReadCraftingDiscipline(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var discipline = new RequiredMember<CraftingDisciplineName>("discipline");
            var rating = new RequiredMember<int>("rating");
            var active = new RequiredMember<bool>("active");

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
}
