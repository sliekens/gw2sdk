using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Commerce.Exchange;

internal static class GemsToGoldJson
{
    public static GemsToGold GetGemsToGold(this in JsonElement json)
    {
        RequiredMember coinsPerGem = "coins_per_gem";
        RequiredMember quantity = "quantity";
        foreach (JsonProperty member in json.EnumerateObject())
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

        return new GemsToGold
        {
            ExchangeRate = coinsPerGem.Map(static (in value) => value.GetInt32()),
            Gold = quantity.Map(static (in value) => value.GetInt32())
        };
    }
}
