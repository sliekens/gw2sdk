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
        RequiredMember<int> tab = new("tab");
        RequiredMember<string> name = new("name");
        RequiredMember<EquipmentItem> equipment = new("equipment");
        RequiredMember<PvpEquipment> pvpEquipment = new("equipment_pvp");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(tab.Name))
            {
                tab.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals("is_active"))
            {
                // Ignore this because you should only use ActiveEquipmentTab
                // => player.EquipmentTabs.Single(tab => tab.Tab == player.ActiveEquipmentTab);

                // Otherwise you have to update two objects when the active tab changes and you can't do that atomically
            }
            else if (member.NameEquals(equipment.Name))
            {
                equipment.Value = member.Value;
            }
            else if (member.NameEquals(pvpEquipment.Name))
            {
                pvpEquipment.Value = member.Value;
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
                pvpEquipment.Select(value => value.GetPvpEquipment(missingMemberBehavior))
        };
    }
}
