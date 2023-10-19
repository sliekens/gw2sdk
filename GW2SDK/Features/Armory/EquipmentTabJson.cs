using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Armory;

[PublicAPI]
public static class EquipmentTabJson
{
    public static EquipmentTab GetEquipmentTab(
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
            if (member.NameEquals(tab.Name))
            {
                tab = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals("is_active"))
            {
                // Ignore this because you should only use ActiveEquipmentTab
                // => player.EquipmentTabs.Single(tab => tab.Tab == player.ActiveEquipmentTab);

                // Otherwise you have to update two objects when the active tab changes and you can't do that atomically
            }
            else if (member.NameEquals(equipment.Name))
            {
                equipment = member;
            }
            else if (member.NameEquals(pvpEquipment.Name))
            {
                pvpEquipment = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EquipmentTab
        {
            Tab = tab.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Equipment =
                equipment.Select(values => values.GetList(value => value.GetEquipmentItem(missingMemberBehavior))),
            PvpEquipment =
                pvpEquipment.Select(value => value.GetPvpEquipment(missingMemberBehavior))
        };
    }
}
