using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class EquipmentTemplateJson
{
    public static EquipmentTemplate GetEquipmentTemplate(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember tab = "tab";
        RequiredMember name = "name";
        RequiredMember equipment = "equipment";
        RequiredMember pvpEquipment = "equipment_pvp";

        foreach (var member in json.EnumerateObject())
        {
            if (tab.Match(member))
            {
                tab = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (member.Name == "is_active")
            {
                // Ignore this because you should only use ActiveEquipmentTab
                // => player.EquipmentTabs.Single(tab => tab.Tab == player.ActiveEquipmentTab);

                // Otherwise you have to update two objects when the active tab changes and you can't do that atomically
            }
            else if (equipment.Match(member))
            {
                equipment = member;
            }
            else if (pvpEquipment.Match(member))
            {
                pvpEquipment = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EquipmentTemplate
        {
            TabNumber = tab.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Items = equipment.Map(
                values => values.GetList(value => value.GetEquipmentItem(missingMemberBehavior))
            ),
            PvpEquipment = pvpEquipment.Map(value => value.GetPvpEquipment(missingMemberBehavior))
        };
    }
}
