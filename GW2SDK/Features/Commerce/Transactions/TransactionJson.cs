﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Transactions;

internal static class TransactionJson
{
    public static Transaction GetTransaction(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember itemId = "item_id";
        RequiredMember price = "price";
        RequiredMember quantity = "quantity";
        RequiredMember created = "created";
        RequiredMember purchased = "purchased";

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
            else if (purchased.Match(member))
            {
                purchased = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Transaction
        {
            Id = id.Map(static value => value.GetInt64()),
            ItemId = itemId.Map(static value => value.GetInt32()),
            UnitPrice = price.Map(static value => value.GetInt32()),
            Quantity = quantity.Map(static value => value.GetInt32()),
            Created = created.Map(static value => value.GetDateTimeOffset()),
            Executed = purchased.Map(static value => value.GetDateTimeOffset())
        };
    }
}
