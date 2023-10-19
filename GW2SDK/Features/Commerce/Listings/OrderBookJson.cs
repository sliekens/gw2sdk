using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Listings;

[PublicAPI]
public static class OrderBookJson
{
    public static OrderBook GetOrderBook(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember demand = "buys";
        RequiredMember supply = "sells";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(demand.Name))
            {
                demand = member;
            }
            else if (member.NameEquals(supply.Name))
            {
                supply = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new OrderBook
        {
            Id = id.Select(value => value.GetInt32()),
            Demand = demand.Select(values => values.GetList(value => value.GetOrderBookLine(missingMemberBehavior))),
            Supply = supply.Select(values => values.GetList(value => value.GetOrderBookLine(missingMemberBehavior)))
        };
    }
}
