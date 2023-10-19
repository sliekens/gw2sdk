﻿using System.Text.Json;
using GuildWars2.Armory;
using GuildWars2.BuildStorage;
using GuildWars2.Crafting;
using GuildWars2.Inventories;
using GuildWars2.Json;
using GuildWars2.Professions;

namespace GuildWars2.Accounts;

[PublicAPI]
public static class CharacterJson
{
    public static Character GetCharacter(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember race = "race";
        RequiredMember gender = "gender";
        RequiredMember flags = "flags";
        RequiredMember profession = "profession";
        RequiredMember level = "level";
        OptionalMember guild = "guild";
        RequiredMember age = "age";
        RequiredMember lastModified = "last_modified";
        RequiredMember created = "created";
        RequiredMember deaths = "deaths";
        RequiredMember crafting = "crafting";
        NullableMember title = "title";
        RequiredMember backstory = "backstory";
        OptionalMember wvwAbilities = "wvw_abilities";
        NullableMember buildTabsUnlocked = "build_tabs_unlocked";
        NullableMember activeBuildTab = "active_build_tab";
        OptionalMember buildTabs = "build_tabs";
        NullableMember equipmentTabsUnlocked = "equipment_tabs_unlocked";
        NullableMember activeEquipmentTab = "active_equipment_tab";
        OptionalMember equipment = "equipment";
        OptionalMember equipmentTabs = "equipment_tabs";
        OptionalMember recipes = "recipes";
        OptionalMember training = "training";
        OptionalMember bags = "bags";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(race.Name))
            {
                race = member;
            }
            else if (member.NameEquals(gender.Name))
            {
                gender = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(guild.Name))
            {
                guild = member;
            }
            else if (member.NameEquals(age.Name))
            {
                age = member;
            }
            else if (member.NameEquals(lastModified.Name))
            {
                lastModified = member;
            }
            else if (member.NameEquals(created.Name))
            {
                created = member;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths = member;
            }
            else if (member.NameEquals(crafting.Name))
            {
                crafting = member;
            }
            else if (member.NameEquals(title.Name))
            {
                title = member;
            }
            else if (member.NameEquals(backstory.Name))
            {
                backstory = member;
            }
            else if (member.NameEquals(wvwAbilities.Name))
            {
                wvwAbilities = member;
            }
            else if (member.NameEquals(buildTabsUnlocked.Name))
            {
                buildTabsUnlocked = member;
            }
            else if (member.NameEquals(activeBuildTab.Name))
            {
                activeBuildTab = member;
            }
            else if (member.NameEquals(buildTabs.Name))
            {
                buildTabs = member;
            }
            else if (member.NameEquals(equipmentTabsUnlocked.Name))
            {
                equipmentTabsUnlocked = member;
            }
            else if (member.NameEquals(activeEquipmentTab.Name))
            {
                activeEquipmentTab = member;
            }
            else if (member.NameEquals(equipment.Name))
            {
                equipment = member;
            }
            else if (member.NameEquals(equipmentTabs.Name))
            {
                equipmentTabs = member;
            }
            else if (member.NameEquals(recipes.Name))
            {
                recipes = member;
            }
            else if (member.NameEquals(training.Name))
            {
                training = member;
            }
            else if (member.NameEquals(bags.Name))
            {
                bags = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Character
        {
            Name = name.Map(value => value.GetStringRequired()),
            Race = race.Map(value => value.GetEnum<RaceName>(missingMemberBehavior)),
            Gender = gender.Map(value => value.GetEnum<Gender>(missingMemberBehavior)),
            Flags =
                flags.Map(
                    values =>
                        values.GetList(value => value.GetEnum<CharacterFlag>(missingMemberBehavior))
                ),
            Level = level.Map(value => value.GetInt32()),
            GuildId = guild.Map(value => value.GetString()) ?? "",
            Profession =
                profession.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Age = age.Map(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Map(value => value.GetDateTimeOffset()),
            Created = created.Map(value => value.GetDateTimeOffset()),
            Deaths = deaths.Map(value => value.GetInt32()),
            CraftingDisciplines =
                crafting.Map(
                    values =>
                        values.GetList(value => value.GetCraftingDiscipline(missingMemberBehavior))
                ),
            TitleId = title.Map(value => value.GetInt32()),
            Backstory = backstory.Map(values => values.GetList(value => value.GetStringRequired())),
            WvwAbilities =
                wvwAbilities.Map(
                    values => values.GetList(value => value.GetWvwAbility(missingMemberBehavior))
                ),
            BuildTabsUnlocked = buildTabsUnlocked.Map(value => value.GetInt32()),
            ActiveBuildTab = activeBuildTab.Map(value => value.GetInt32()),
            BuildTabs =
                buildTabs.Map(
                    values => values.GetList(value => value.GetBuildTab(missingMemberBehavior))
                ),
            EquipmentTabsUnlocked = equipmentTabsUnlocked.Map(value => value.GetInt32()),
            ActiveEquipmentTab = activeEquipmentTab.Map(value => value.GetInt32()),
            Equipment =
                equipment.Map(
                    values => values.GetList(value => value.GetEquipmentItem(missingMemberBehavior))
                ),
            EquipmentTabs =
                equipmentTabs.Map(
                    values => values.GetList(value => value.GetEquipmentTab(missingMemberBehavior))
                ),
            Recipes = recipes.Map(values => values.GetList(value => value.GetInt32())),
            Training =
                training.Map(
                    values => values.GetList(
                        value => value.GetTrainingProgress(missingMemberBehavior)
                    )
                ),
            Bags = bags.Map(values => values.GetList(value => value.GetBag(missingMemberBehavior)))
        };
    }
}
