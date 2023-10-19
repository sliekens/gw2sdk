﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Transactions;

[PublicAPI]
public static class OrderJson
{
    public static Order GetOrder(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember itemId = new("item_id");
        RequiredMember price = new("price");
        RequiredMember quantity = new("quantity");
        RequiredMember created = new("created");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (member.NameEquals(price.Name))
            {
                price.Value = member.Value;
            }
            else if (member.NameEquals(quantity.Name))
            {
                quantity.Value = member.Value;
            }
            else if (member.NameEquals(created.Name))
            {
                created.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Order
        {
            Id = id.Select(value => value.GetInt64()),
            ItemId = itemId.Select(value => value.GetInt32()),
            Price = price.Select(value => value.GetInt32()),
            Quantity = quantity.Select(value => value.GetInt32()),
            Created = created.Select(value => value.GetDateTimeOffset())
        };
    }
}
