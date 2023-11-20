using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Exchange;

internal static class GemsToGoldJson
{
    public static GemsToGold GetGemsToGold(
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

        return new GemsToGold
        {
            ExchangeRate = coinsPerGem.Map(value => value.GetInt32()),
            Gold = quantity.Map(value => value.GetInt32())
        };
    }
}
