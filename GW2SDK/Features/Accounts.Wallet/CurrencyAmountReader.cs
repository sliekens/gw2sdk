using GW2SDK.Accounts.Wallet;
using GW2SDK.Json;
using JetBrains.Annotations;
using System;
using System.Text.Json;

namespace GW2SDK.Features.Accounts.Wallet
{
    [PublicAPI]
    public sealed class CurrencyAmountReader : IJsonReader<CurrencyAmount>
    {
        public CurrencyAmount Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
