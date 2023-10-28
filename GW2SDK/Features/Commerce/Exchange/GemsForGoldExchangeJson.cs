using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Exchange;

internal static class GemsForGoldExchangeJson
{
    public static GemsForGoldExchange GetGemsForGoldExchange(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coinsPerGem = "coins_per_gem";
        RequiredMember quantity = "quantity";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == coinsPerGem.Name)
            {
                coinsPerGem = member;
            }
            else if (member.Name == quantity.Name)
            {
                quantity = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GemsForGoldExchange
        {
            CoinsPerGem = coinsPerGem.Map(value => value.GetInt32()),
            CoinsToReceive = quantity.Map(value => value.GetInt32())
        };
    }
}
