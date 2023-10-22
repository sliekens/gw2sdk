using System.Text.Json;
using GuildWars2.Inventories;

namespace GuildWars2.Banking;

internal static class BankJson
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
