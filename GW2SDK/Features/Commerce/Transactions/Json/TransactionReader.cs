using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Transactions.Json
{
    [PublicAPI]
    public static class TransactionReader
    {
        public static Transaction Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<long>("id");
            var itemId = new RequiredMember<int>("item_id");
            var price = new RequiredMember<Coin>("price");
            var quantity = new RequiredMember<int>("quantity");
            var created = new RequiredMember<DateTimeOffset>("created");
            var purchased = new RequiredMember<DateTimeOffset>("purchased");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(itemId.Name))
                {
                    itemId = itemId.From(member.Value);
                }
                else if (member.NameEquals(price.Name))
                {
                    price = price.From(member.Value);
                }
                else if (member.NameEquals(quantity.Name))
                {
                    quantity = quantity.From(member.Value);
                }
                else if (member.NameEquals(created.Name))
                {
                    created = created.From(member.Value);
                }
                else if (member.NameEquals(purchased.Name))
                {
                    purchased = purchased.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Transaction
            {
                Id = id.GetValue(),
                ItemId = itemId.GetValue(),
                Price = price.GetValue(),
                Quantity = quantity.GetValue(),
                Created = created.GetValue(),
                Executed = purchased.GetValue()
            };
        }
    }
}
