﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Transactions;

internal static class TransactionJson
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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == itemId.Name)
            {
                itemId = member;
            }
            else if (member.Name == price.Name)
            {
                price = member;
            }
            else if (member.Name == quantity.Name)
            {
                quantity = member;
            }
            else if (member.Name == created.Name)
            {
                created = member;
            }
            else if (member.Name == purchased.Name)
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
            Id = id.Map(value => value.GetInt64()),
            ItemId = itemId.Map(value => value.GetInt32()),
            UnitPrice = price.Map(value => value.GetInt32()),
            Quantity = quantity.Map(value => value.GetInt32()),
            Created = created.Map(value => value.GetDateTimeOffset()),
            Executed = purchased.Map(value => value.GetDateTimeOffset())
        };
    }
}
