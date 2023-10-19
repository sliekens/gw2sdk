using System.Text.Json;
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
        RequiredMember name = new("name");
        RequiredMember race = new("race");
        RequiredMember gender = new("gender");
        RequiredMember flags = new("flags");
        RequiredMember profession = new("profession");
        RequiredMember level = new("level");
        OptionalMember guild = new("guild");
        RequiredMember age = new("age");
        RequiredMember lastModified = new("last_modified");
        RequiredMember created = new("created");
        RequiredMember deaths = new("deaths");
        RequiredMember crafting = new("crafting");
        NullableMember title = new("title");
        RequiredMember backstory = new("backstory");
        OptionalMember wvwAbilities = new("wvw_abilities");
        NullableMember buildTabsUnlocked = new("build_tabs_unlocked");
        NullableMember activeBuildTab = new("active_build_tab");
        OptionalMember buildTabs = new("build_tabs");
        NullableMember equipmentTabsUnlocked = new("equipment_tabs_unlocked");
        NullableMember activeEquipmentTab = new("active_equipment_tab");
        OptionalMember equipment = new("equipment");
        OptionalMember equipmentTabs = new("equipment_tabs");
        OptionalMember recipes = new("recipes");
        OptionalMember training = new("training");
        OptionalMember bags = new("bags");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(race.Name))
            {
                race.Value = member.Value;
            }
            else if (member.NameEquals(gender.Name))
            {
                gender.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(guild.Name))
            {
                guild.Value = member.Value;
            }
            else if (member.NameEquals(age.Name))
            {
                age.Value = member.Value;
            }
            else if (member.NameEquals(lastModified.Name))
            {
                lastModified.Value = member.Value;
            }
            else if (member.NameEquals(created.Name))
            {
                created.Value = member.Value;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths.Value = member.Value;
            }
            else if (member.NameEquals(crafting.Name))
            {
                crafting.Value = member.Value;
            }
            else if (member.NameEquals(title.Name))
            {
                title.Value = member.Value;
            }
            else if (member.NameEquals(backstory.Name))
            {
                backstory.Value = member.Value;
            }
            else if (member.NameEquals(wvwAbilities.Name))
            {
                wvwAbilities.Value = member.Value;
            }
            else if (member.NameEquals(buildTabsUnlocked.Name))
            {
                buildTabsUnlocked.Value = member.Value;
            }
            else if (member.NameEquals(activeBuildTab.Name))
            {
                activeBuildTab.Value = member.Value;
            }
            else if (member.NameEquals(buildTabs.Name))
            {
                buildTabs.Value = member.Value;
            }
            else if (member.NameEquals(equipmentTabsUnlocked.Name))
            {
                equipmentTabsUnlocked.Value = member.Value;
            }
            else if (member.NameEquals(activeEquipmentTab.Name))
            {
                activeEquipmentTab.Value = member.Value;
            }
            else if (member.NameEquals(equipment.Name))
            {
                equipment.Value = member.Value;
            }
            else if (member.NameEquals(equipmentTabs.Name))
            {
                equipmentTabs.Value = member.Value;
            }
            else if (member.NameEquals(recipes.Name))
            {
                recipes.Value = member.Value;
            }
            else if (member.NameEquals(training.Name))
            {
                training.Value = member.Value;
            }
            else if (member.NameEquals(bags.Name))
            {
                bags.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Character
        {
            Name = name.Select(value => value.GetStringRequired()),
            Race = race.Select(value => value.GetEnum<RaceName>(missingMemberBehavior)),
            Gender = gender.Select(value => value.GetEnum<Gender>(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<CharacterFlag>(missingMemberBehavior)),
            Level = level.Select(value => value.GetInt32()),
            GuildId = guild.Select(value => value.GetString()) ?? "",
            Profession = profession.Select(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Age = age.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Select(value => value.GetDateTimeOffset()),
            Created = created.Select(value => value.GetDateTimeOffset()),
            Deaths = deaths.Select(value => value.GetInt32()),
            CraftingDisciplines =
                crafting.SelectMany(value => value.GetCraftingDiscipline(missingMemberBehavior)),
            TitleId = title.Select(value => value.GetInt32()),
            Backstory = backstory.SelectMany(value => value.GetStringRequired()),
            WvwAbilities =
                wvwAbilities.SelectMany(value => value.GetWvwAbility(missingMemberBehavior)),
            BuildTabsUnlocked = buildTabsUnlocked.Select(value => value.GetInt32()),
            ActiveBuildTab = activeBuildTab.Select(value => value.GetInt32()),
            BuildTabs = buildTabs.SelectMany(value => value.GetBuildTab(missingMemberBehavior)),
            EquipmentTabsUnlocked = equipmentTabsUnlocked.Select(value => value.GetInt32()),
            ActiveEquipmentTab = activeEquipmentTab.Select(value => value.GetInt32()),
            Equipment =
                equipment.SelectMany(value => value.GetEquipmentItem(missingMemberBehavior)),
            EquipmentTabs =
                equipmentTabs.SelectMany(value => value.GetEquipmentTab(missingMemberBehavior)),
            Recipes = recipes.SelectMany(value => value.GetInt32()),
            Training =
                training.SelectMany(value => value.GetTrainingProgress(missingMemberBehavior)),
            Bags = bags.SelectMany(value => value.GetBag(missingMemberBehavior))
        };
    }
}
