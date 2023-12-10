using System.Text.Json;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Hero.Inventories;
using GuildWars2.Hero.Races;
using GuildWars2.Hero.Training;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class CharacterJson
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
        OptionalMember equippedItems = "equipment";
        OptionalMember equipmentTabs = "equipment_tabs";
        OptionalMember recipes = "recipes";
        OptionalMember training = "training";
        OptionalMember bags = "bags";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == race.Name)
            {
                race = member;
            }
            else if (member.Name == gender.Name)
            {
                gender = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == profession.Name)
            {
                profession = member;
            }
            else if (member.Name == level.Name)
            {
                level = member;
            }
            else if (member.Name == guild.Name)
            {
                guild = member;
            }
            else if (member.Name == age.Name)
            {
                age = member;
            }
            else if (member.Name == lastModified.Name)
            {
                lastModified = member;
            }
            else if (member.Name == created.Name)
            {
                created = member;
            }
            else if (member.Name == deaths.Name)
            {
                deaths = member;
            }
            else if (member.Name == crafting.Name)
            {
                crafting = member;
            }
            else if (member.Name == title.Name)
            {
                title = member;
            }
            else if (member.Name == backstory.Name)
            {
                backstory = member;
            }
            else if (member.Name == wvwAbilities.Name)
            {
                wvwAbilities = member;
            }
            else if (member.Name == buildTabsUnlocked.Name)
            {
                buildTabsUnlocked = member;
            }
            else if (member.Name == activeBuildTab.Name)
            {
                activeBuildTab = member;
            }
            else if (member.Name == buildTabs.Name)
            {
                buildTabs = member;
            }
            else if (member.Name == equipmentTabsUnlocked.Name)
            {
                equipmentTabsUnlocked = member;
            }
            else if (member.Name == activeEquipmentTab.Name)
            {
                activeEquipmentTab = member;
            }
            else if (member.Name == equippedItems.Name)
            {
                equippedItems = member;
            }
            else if (member.Name == equipmentTabs.Name)
            {
                equipmentTabs = member;
            }
            else if (member.Name == recipes.Name)
            {
                recipes = member;
            }
            else if (member.Name == training.Name)
            {
                training = member;
            }
            else if (member.Name == bags.Name)
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
            Flags = flags.Map(values => values.GetCharacterFlags()),
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
            BuildTemplatesCount = buildTabsUnlocked.Map(value => value.GetInt32()),
            ActiveBuildTemplateNumber = activeBuildTab.Map(value => value.GetInt32()),
            BuildTemplates =
                buildTabs.Map(
                    values => values.GetList(value => value.GetBuildTemplate(missingMemberBehavior))
                ),
            EquipmentTemplatesCount = equipmentTabsUnlocked.Map(value => value.GetInt32()),
            ActiveEquipmentTemplateNumber = activeEquipmentTab.Map(value => value.GetInt32()),
            EquippedItems =
                equippedItems.Map(
                    values => values.GetList(value => value.GetEquipmentItem(missingMemberBehavior))
                ),
            EquipmentTemplates =
                equipmentTabs.Map(
                    values => values.GetList(
                        value => value.GetEquipmentTemplate(missingMemberBehavior)
                    )
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
