using System.Text.Json;
using GuildWars2.Hero.Inventories;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class BankJson
{
    public static Bank
        GetBank(this JsonElement json, MissingMemberBehavior missingMemberBehavior) =>
        new() { Items = json.GetList(value => value.GetItemSlot(missingMemberBehavior)) };
}
