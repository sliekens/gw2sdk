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
        RequiredMember<int> id = new("id");
        RequiredMember<OrderBookLine> demand = new("buys");
        RequiredMember<OrderBookLine> supply = new("sells");

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
            Id = id.GetValue(),
            Demand = demand.SelectMany(value => value.GetOrderBookLine(missingMemberBehavior)),
            Supply = supply.SelectMany(value => value.GetOrderBookLine(missingMemberBehavior))
        };
    }
}
