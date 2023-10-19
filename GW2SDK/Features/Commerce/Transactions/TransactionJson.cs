﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Transactions;

[PublicAPI]
public static class TransactionJson
{
    public static Transaction GetTransaction(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember itemId = "item_id";
        RequiredMember price = "price";
        RequiredMember quantity = "quantity";
        RequiredMember created = "created";
        RequiredMember purchased = "purchased";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId = member;
            }
            else if (member.NameEquals(price.Name))
            {
                price = member;
            }
            else if (member.NameEquals(quantity.Name))
            {
                quantity = member;
            }
            else if (member.NameEquals(created.Name))
            {
                created = member;
            }
            else if (member.NameEquals(purchased.Name))
            {
                purchased = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Transaction
        {
            Id = id.Select(value => value.GetInt64()),
            ItemId = itemId.Select(value => value.GetInt32()),
            Price = price.Select(value => value.GetInt32()),
            Quantity = quantity.Select(value => value.GetInt32()),
            Created = created.Select(value => value.GetDateTimeOffset()),
            Executed = purchased.Select(value => value.GetDateTimeOffset())
        };
    }
}
