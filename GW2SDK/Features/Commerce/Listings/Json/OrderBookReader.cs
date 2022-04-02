using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings.Json
{
    [PublicAPI]
    public static class OrderBookReader
    {
        public static OrderBook Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var demand = new RequiredMember<OrderBookLine>("buys");
            var supply = new RequiredMember<OrderBookLine>("sells");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(demand.Name))
                {
                    demand = demand.From(member.Value);
                }
                else if (member.NameEquals(supply.Name))
                {
                    supply = supply.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new OrderBook
            {
                Id = id.GetValue(),
                Demand = demand.SelectMany(value => ReadItemListingLine(value, missingMemberBehavior)),
                Supply = supply.SelectMany(value => ReadItemListingLine(value, missingMemberBehavior))
            };
        }

        private static OrderBookLine ReadItemListingLine(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var listings = new RequiredMember<int>("listings");
            var unitPrice = new RequiredMember<int>("unit_price");
            var quantity = new RequiredMember<int>("quantity");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(listings.Name))
                {
                    listings = listings.From(member.Value);
                }
                else if (member.NameEquals(unitPrice.Name))
                {
                    unitPrice = unitPrice.From(member.Value);
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

            return new OrderBookLine
            {
                Listings = listings.GetValue(),
                UnitPrice = unitPrice.GetValue(),
                Quantity = quantity.GetValue()
            };
        }
    }
}
