using System.Text.Json;

namespace GuildWars2.Inventories;

internal static class InventoryJson
{
    public static Inventory GetInventory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        List<ItemSlot?> slots = new(json.GetArrayLength());

        slots.AddRange(
            json.EnumerateArray().Select(entry => entry.GetItemSlot(missingMemberBehavior))
        );

        return new Inventory(slots);
    }
}
