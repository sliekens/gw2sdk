using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Wallet.Json
{
    [PublicAPI]
    public static class CurrencyAmountReader
    {
        public static CurrencyAmount Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var value = new RequiredMember<int>("value");

            foreach(var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(value.Name))
                {
                    value = value.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new CurrencyAmount
            {
                CurrencyId = id.GetValue(),
                Amount = value.GetValue()
            };
        }
    }
}
