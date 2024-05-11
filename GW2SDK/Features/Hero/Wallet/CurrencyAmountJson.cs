using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Wallet;

internal static class CurrencyAmountJson
{
    public static CurrencyAmount GetCurrencyAmount(this JsonElement json)
    {
        RequiredMember currencyId = "id";
        RequiredMember amount = "value";

        foreach (var member in json.EnumerateObject())
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CurrencyAmount
        {
            CurrencyId = currencyId.Map(static value => value.GetInt32()),
            Amount = amount.Map(static value => value.GetInt32())
        };
    }
}
