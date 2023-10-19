using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Currencies;

[PublicAPI]
public static class CurrencyAmountJson
{
    public static CurrencyAmount GetCurrencyAmount(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember currencyId = "id";
        RequiredMember amount = "value";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(currencyId.Name))
            {
                currencyId = member;
            }
            else if (member.NameEquals(amount.Name))
            {
                amount = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CurrencyAmount
        {
            CurrencyId = currencyId.Map(value => value.GetInt32()),
            Amount = amount.Map(value => value.GetInt32())
        };
    }
}
