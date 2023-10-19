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
        RequiredMember coinsPerGem = new("coins_per_gem");
        RequiredMember quantity = new("quantity");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coinsPerGem.Name))
            {
                coinsPerGem.Value = member.Value;
            }
            else if (member.NameEquals(quantity.Name))
            {
                quantity.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GoldForGemsExchange
        {
            CoinsPerGem = coinsPerGem.Select(value => value.GetInt32()),
            GemsToReceive = quantity.Select(value => value.GetInt32())
        };
    }
}
