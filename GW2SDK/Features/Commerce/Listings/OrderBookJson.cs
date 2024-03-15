using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Listings;

internal static class OrderBookJson
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (demand.Match(member))
            {
                demand = member;
            }
            else if (supply.Match(member))
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
            Id = id.Map(value => value.GetInt32()),
            Demand =
                demand.Map(
                    values => values.GetList(value => value.GetOrderBookLine(missingMemberBehavior))
                ),
            Supply = supply.Map(
                values => values.GetList(value => value.GetOrderBookLine(missingMemberBehavior))
            )
        };
    }
}
