using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GW2SDK.Accounts.Inventories;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banking;

[PublicAPI]
public static class BankReader
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
