﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Listings;

internal static class OrderBookLineJson
{
    public static OrderBookLine GetOrderBookLine(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember listings = "listings";
        RequiredMember unitPrice = "unit_price";
        RequiredMember quantity = "quantity";

        foreach (var member in json.EnumerateObject())
        {
            if (listings.Match(member))
            {
                listings = member;
            }
            else if (unitPrice.Match(member))
            {
                unitPrice = member;
            }
            else if (quantity.Match(member))
            {
                quantity = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new OrderBookLine
        {
            Listings = listings.Map(value => value.GetInt32()),
            UnitPrice = unitPrice.Map(value => value.GetInt32()),
            Quantity = quantity.Map(value => value.GetInt32())
        };
    }
}
