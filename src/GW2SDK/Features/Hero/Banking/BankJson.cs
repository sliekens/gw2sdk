using System.Text.Json;

using GuildWars2.Hero.Inventories;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class BankJson
{
    public static Bank GetBank(this in JsonElement json)
    {
        return new() { Items = json.GetList(static (in value) => value.GetItemSlot()) };
    }
}
