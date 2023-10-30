using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Inventories;

internal static class InventoryJson
{
    public static Inventory GetInventory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    ) =>
        new() { Items = json.GetList(value => value.GetItemSlot(missingMemberBehavior)) };
}
