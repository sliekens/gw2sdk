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
        RequiredMember id = new("id");
        RequiredMember demand = new("buys");
        RequiredMember supply = new("sells");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(demand.Name))
            {
                demand.Value = member.Value;
            }
            else if (member.NameEquals(supply.Name))
            {
                supply.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new OrderBook
        {
            Id = id.Select(value => value.GetInt32()),
            Demand = demand.SelectMany(value => value.GetOrderBookLine(missingMemberBehavior)),
            Supply = supply.SelectMany(value => value.GetOrderBookLine(missingMemberBehavior))
        };
    }
}
