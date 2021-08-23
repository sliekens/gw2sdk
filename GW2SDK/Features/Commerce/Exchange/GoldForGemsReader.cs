using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Exchange
{
    [PublicAPI]
    public sealed class GoldForGemsReader : IJsonReader<GoldForGemsExchange>
    {
        public GoldForGemsExchange Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var coinsPerGem = new RequiredMember<int>("coins_per_gem");
            var quantity = new RequiredMember<int>("quantity");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(coinsPerGem.Name))
                {
                    coinsPerGem = coinsPerGem.From(member.Value);
                }
                else if (member.NameEquals(quantity.Name))
                {
                    quantity = quantity.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new GoldForGemsExchange
            {
                CoinsPerGem = coinsPerGem.GetValue(),
                GemsToReceive = quantity.GetValue()
            };
        }
    }
}
