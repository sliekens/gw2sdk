using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Inventories;

[PublicAPI]
public static class InventoryReader
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
