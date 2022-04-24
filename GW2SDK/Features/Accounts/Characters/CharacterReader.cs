﻿using System;
using System.Text.Json;
using GW2SDK.Accounts.BuildStorage;
using GW2SDK.Accounts.Characters.Armory;
using GW2SDK.Accounts.Characters.Inventories;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Characters;

[PublicAPI]
public static class CharacterReader
{
    public static Character GetCharacter(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            CraftingDisciplines =
                crafting.SelectMany(value => ReadCraftingDiscipline(value, missingMemberBehavior)),
            TitleId = title.GetValue(),
            Backstory = backstory.SelectMany(value => value.GetStringRequired()),
            WvwAbilities =
                wvwAbilities.SelectMany(value => ReadWvwAbility(value, missingMemberBehavior)),
            BuildTabsUnlocked = buildTabsUnlocked.GetValue(),
            ActiveBuildTab = activeBuildTab.GetValue(),
            BuildTabs = buildTabs.SelectMany(value => ReadBuildTab(value, missingMemberBehavior)),
            EquipmentTabsUnlocked = equipmentTabsUnlocked.GetValue(),
            ActiveEquipmentTab = activeEquipmentTab.GetValue(),
            Equipment =
                equipment.SelectMany(value => value.GetEquipmentItem(missingMemberBehavior)),
            EquipmentTabs =
                equipmentTabs.SelectMany(value => ReadEquipmentTab(value, missingMemberBehavior)),
            Recipes = recipes.SelectMany(value => value.GetInt32()),
            Training =
                training.SelectMany(value => ReadTrainingObjective(value, missingMemberBehavior)),
            Bags = bags.SelectMany(value => value.GetBag(missingMemberBehavior))
        };
    }

    private static TrainingTrack ReadTrainingObjective(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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

    private static EquipmentTab ReadEquipmentTab(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            Equipment =
                equipment.SelectMany(value => value.GetEquipmentItem(missingMemberBehavior)),
            PvpEquipment =
                pvpEquipment.Select(value => ReadPvpEquipment(value, missingMemberBehavior))
        };
    }

    private static PvpEquipment ReadPvpEquipment(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            SigilIds = sigils.SelectMany(
                value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
                )
        };
    }

    private static BuildTab ReadBuildTab(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            Build = build.Select(value => value.GetBuild(missingMemberBehavior))
        };
    }

    private static WvwAbility ReadWvwAbility(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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