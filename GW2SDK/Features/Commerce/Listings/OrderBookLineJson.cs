using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Listings;

[PublicAPI]
public static class OrderBookLineJson
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
            if (member.NameEquals(listings.Name))
            {
                listings = member;
            }
            else if (member.NameEquals(unitPrice.Name))
            {
                unitPrice = member;
            }
            else if (member.NameEquals(quantity.Name))
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
            Listings = listings.Select(value => value.GetInt32()),
            UnitPrice = unitPrice.Select(value => value.GetInt32()),
            Quantity = quantity.Select(value => value.GetInt32())
        };
    }
}
