using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GW2SDK.Inventories;
using JetBrains.Annotations;

namespace GW2SDK.Banking;

[PublicAPI]
public static class BankJson
{
    public static Bank GetBank(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        List<ItemSlot?> slots = new(json.GetArrayLength());

        slots.AddRange(
            json.EnumerateArray().Select(entry => entry.GetItemSlot(missingMemberBehavior))
        );

        return new Bank(slots);
    }
}
