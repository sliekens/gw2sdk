using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Exchange;

internal static class GoldToGemsJson
{
    public static GoldToGems GetGoldToGems(
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

        return new GoldToGems
        {
            ExchangeRate = coinsPerGem.Map(value => value.GetInt32()),
            Gems = quantity.Map(value => value.GetInt32())
        };
    }
}
