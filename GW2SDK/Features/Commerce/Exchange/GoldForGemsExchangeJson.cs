using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Exchange;

[PublicAPI]
public static class GoldForGemsExchangeJson
{
    public static GoldForGemsExchange GetGoldForGemsExchange(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coinsPerGem = "coins_per_gem";
        RequiredMember quantity = "quantity";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coinsPerGem.Name))
            {
                coinsPerGem = member;
            }
            else if (member.NameEquals(quantity.Name))
            {
                quantity = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GoldForGemsExchange
        {
            CoinsPerGem = coinsPerGem.Map(value => value.GetInt32()),
            GemsToReceive = quantity.Map(value => value.GetInt32())
        };
    }
}
