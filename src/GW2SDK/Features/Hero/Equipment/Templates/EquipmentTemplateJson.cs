using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class EquipmentTemplateJson
{
    public static EquipmentTemplate GetEquipmentTemplate(this in JsonElement json)
    {
        RequiredMember tab = "tab";
        RequiredMember name = "name";
        RequiredMember equipment = "equipment";
        RequiredMember pvpEquipment = "equipment_pvp";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (tab.Match(member))
            {
                tab = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (member.NameEquals("is_active"))
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new EquipmentTemplate
        {
            TabNumber = tab.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Items = equipment.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetEquipmentItem())
            ),
            PvpEquipment = pvpEquipment.Map(static (in JsonElement value) => value.GetPvpEquipment())
        };
    }
}
