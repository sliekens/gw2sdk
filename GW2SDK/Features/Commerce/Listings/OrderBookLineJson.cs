using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Commerce.Listings;

[PublicAPI]
public static class OrderBookLineJson
{
    public static OrderBookLine GetOrderBookLine(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> listings = new("listings");
        RequiredMember<int> unitPrice = new("unit_price");
        RequiredMember<int> quantity = new("quantity");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(listings.Name))
            {
                listings.Value = member.Value;
            }
            else if (member.NameEquals(unitPrice.Name))
            {
                unitPrice.Value = member.Value;
            }
            else if (member.NameEquals(quantity.Name))
            {
                quantity.Value = member.Value;
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
