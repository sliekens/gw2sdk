using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Wallet;

internal static class CurrencyAmountJson
{
    public static CurrencyAmount GetCurrencyAmount(this in JsonElement json)
    {
        RequiredMember currencyId = "id";
        RequiredMember amount = "value";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (currencyId.Match(member))
            {
                currencyId = member;
            }
            else if (amount.Match(member))
            {
                amount = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CurrencyAmount
        {
            CurrencyId = currencyId.Map(static (in JsonElement value) => value.GetInt32()),
            Amount = amount.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
