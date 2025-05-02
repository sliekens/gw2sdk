using System.Text.Json;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Hero.Inventories;
using GuildWars2.Hero.Training;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class CharacterJson
{
    public static Character GetCharacter(this JsonElement json)
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
            if (name.Match(member))
            {
                name = member;
            }
            else if (race.Match(member))
            {
                race = member;
            }
            else if (gender.Match(member))
            {
                gender = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (profession.Match(member))
            {
                profession = member;
            }
            else if (level.Match(member))
            {
                level = member;
            }
            else if (guild.Match(member))
            {
                guild = member;
            }
            else if (age.Match(member))
            {
                age = member;
            }
            else if (lastModified.Match(member))
            {
                lastModified = member;
            }
            else if (created.Match(member))
            {
                created = member;
            }
            else if (deaths.Match(member))
            {
                deaths = member;
            }
            else if (crafting.Match(member))
            {
                crafting = member;
            }
            else if (title.Match(member))
            {
                title = member;
            }
            else if (backstory.Match(member))
            {
                backstory = member;
            }
            else if (wvwAbilities.Match(member))
            {
                wvwAbilities = member;
            }
            else if (buildTabsUnlocked.Match(member))
            {
                buildTabsUnlocked = member;
            }
            else if (activeBuildTab.Match(member))
            {
                activeBuildTab = member;
            }
            else if (buildTabs.Match(member))
            {
                buildTabs = member;
            }
            else if (equipmentTabsUnlocked.Match(member))
            {
                equipmentTabsUnlocked = member;
            }
            else if (activeEquipmentTab.Match(member))
            {
                activeEquipmentTab = member;
            }
            else if (equippedItems.Match(member))
            {
                equippedItems = member;
            }
            else if (equipmentTabs.Match(member))
            {
                equipmentTabs = member;
            }
            else if (recipes.Match(member))
            {
                recipes = member;
            }
            else if (training.Match(member))
            {
                training = member;
            }
            else if (bags.Match(member))
            {
                bags = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Character
        {
            Name = name.Map(static value => value.GetStringRequired()),
            Race = race.Map(static value => value.GetEnum<RaceName>()),
            BodyType = gender.Map(static value => value.GetEnum<BodyType>()),
            Flags = flags.Map(static values => values.GetCharacterFlags()),
            Level = level.Map(static value => value.GetInt32()),
            GuildId = guild.Map(static value => value.GetString()) ?? "",
            Profession = profession.Map(static value => value.GetEnum<ProfessionName>()),
            Age = age.Map(static value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Map(static value => value.GetDateTimeOffset()),
            Created = created.Map(static value => value.GetDateTimeOffset()),
            Deaths = deaths.Map(static value => value.GetInt32()),
            CraftingDisciplines =
                crafting.Map(static values =>
                    values.GetList(static value => value.GetCraftingDiscipline())
                ),
            TitleId = title.Map(static value => value.GetInt32()),
            Backstory =
                backstory.Map(static values =>
                    values.GetList(static value => value.GetStringRequired())
                ),
            WvwAbilities =
                wvwAbilities.Map(static values =>
                    values.GetList(static value => value.GetWvwAbility())
                ),
            BuildTemplatesCount = buildTabsUnlocked.Map(static value => value.GetInt32()),
            ActiveBuildTemplateNumber = activeBuildTab.Map(static value => value.GetInt32()),
            BuildTemplates =
                buildTabs.Map(static values =>
                    values.GetList(static value => value.GetBuildTemplate())
                ),
            EquipmentTemplatesCount = equipmentTabsUnlocked.Map(static value => value.GetInt32()),
            ActiveEquipmentTemplateNumber =
                activeEquipmentTab.Map(static value => value.GetInt32()),
            EquippedItems =
                equippedItems.Map(static values =>
                    values.GetList(static value => value.GetEquipmentItem())
                ),
            EquipmentTemplates =
                equipmentTabs.Map(static values =>
                    values.GetList(static value => value.GetEquipmentTemplate())
                ),
            Recipes =
                recipes.Map(static values => values.GetList(static value => value.GetInt32())),
            Training =
                training.Map(static values =>
                    values.GetList(static value => value.GetTrainingProgress())
                ),
            Bags = bags.Map(static values => values.GetList(static value => value.GetBag()))
        };
    }
}
