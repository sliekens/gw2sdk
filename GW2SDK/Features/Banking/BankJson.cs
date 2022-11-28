using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GuildWars2.Inventories;
using JetBrains.Annotations;

namespace GuildWars2.Banking;

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
