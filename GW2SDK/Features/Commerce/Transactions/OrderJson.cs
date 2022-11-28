﻿using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Commerce.Transactions;

[PublicAPI]
public static class OrderJson
{
    public static Order GetOrder(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<long> id = new("id");
        RequiredMember<int> itemId = new("item_id");
        RequiredMember<Coin> price = new("price");
        RequiredMember<int> quantity = new("quantity");
        RequiredMember<DateTimeOffset> created = new("created");

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
            Id = id.GetValue(),
            ItemId = itemId.GetValue(),
            Price = price.GetValue(),
            Quantity = quantity.GetValue(),
            Created = created.GetValue()
        };
    }
}
