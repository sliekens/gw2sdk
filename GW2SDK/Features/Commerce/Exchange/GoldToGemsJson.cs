using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Exchange;

internal static class GoldToGemsJson
{
    public static GoldToGems GetGoldToGems(this JsonElement json)
    {
        RequiredMember coinsPerGem = "coins_per_gem";
        RequiredMember quantity = "quantity";
        foreach (var member in json.EnumerateObject())
        {
            if (coinsPerGem.Match(member))
            {
                coinsPerGem = member;
            }
            else if (quantity.Match(member))
            {
                quantity = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GoldToGems
        {
            ExchangeRate = coinsPerGem.Map(static value => value.GetInt32()),
            Gems = quantity.Map(static value => value.GetInt32())
        };
    }
}
