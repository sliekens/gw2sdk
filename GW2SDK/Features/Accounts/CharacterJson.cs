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
        RequiredMember<string> name = new("name");
        RequiredMember<RaceName> race = new("race");
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
        OptionalMember<TrainingProgress> training = new("training");
        OptionalMember<Bag?> bags = new("bags");

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
                crafting.SelectMany(value => value.GetCraftingDiscipline(missingMemberBehavior)),
            TitleId = title.GetValue(),
            Backstory = backstory.SelectMany(value => value.GetStringRequired()),
            WvwAbilities =
                wvwAbilities.SelectMany(value => value.GetWvwAbility(missingMemberBehavior)),
            BuildTabsUnlocked = buildTabsUnlocked.GetValue(),
            ActiveBuildTab = activeBuildTab.GetValue(),
            BuildTabs = buildTabs.SelectMany(value => value.GetBuildTab(missingMemberBehavior)),
            EquipmentTabsUnlocked = equipmentTabsUnlocked.GetValue(),
            ActiveEquipmentTab = activeEquipmentTab.GetValue(),
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
