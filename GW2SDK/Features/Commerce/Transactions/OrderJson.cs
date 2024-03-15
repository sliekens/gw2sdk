﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Transactions;

internal static class OrderJson
{
    public static Order GetOrder(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember itemId = "item_id";
        RequiredMember price = "price";
        RequiredMember quantity = "quantity";
        RequiredMember created = "created";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (price.Match(member))
            {
                price = member;
            }
            else if (quantity.Match(member))
            {
                quantity = member;
            }
            else if (created.Match(member))
            {
                created = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Order
        {
            Id = id.Map(value => value.GetInt64()),
            ItemId = itemId.Map(value => value.GetInt32()),
            UnitPrice = price.Map(value => value.GetInt32()),
            Quantity = quantity.Map(value => value.GetInt32()),
            Created = created.Map(value => value.GetDateTimeOffset())
        };
    }
}
