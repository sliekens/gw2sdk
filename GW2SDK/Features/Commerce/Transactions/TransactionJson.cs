using System.Text.Json;
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
        RequiredMember id = new("id");
        RequiredMember itemId = new("item_id");
        RequiredMember price = new("price");
        RequiredMember quantity = new("quantity");
        RequiredMember created = new("created");
        RequiredMember purchased = new("purchased");

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
            else if (member.NameEquals(purchased.Name))
            {
                purchased.Value = member.Value;
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
