using System.Text.Json;
using GuildWars2.Hero.Inventories;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class BankJson
{
    public static Bank GetBank(this JsonElement json)
    {
        return new Bank { Items = json.GetList(static value => value.GetItemSlot()) };
    }
}
