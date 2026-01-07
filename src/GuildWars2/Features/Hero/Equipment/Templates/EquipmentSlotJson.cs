using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class EquipmentSlotJson
{
    public static Extensible<EquipmentSlot> GetEquipmentSlot(this in JsonElement json)
    {
        if (json.ValueEquals("Backpack"))
        {
            return EquipmentSlot.Back;
        }

        return json.GetEnum<EquipmentSlot>();
    }
}
