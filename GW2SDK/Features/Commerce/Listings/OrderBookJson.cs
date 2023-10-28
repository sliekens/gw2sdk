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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == demand.Name)
            {
                demand = member;
            }
            else if (member.Name == supply.Name)
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
