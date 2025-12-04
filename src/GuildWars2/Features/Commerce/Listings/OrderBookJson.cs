using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Commerce.Listings;

internal static class OrderBookJson
{
    public static OrderBook GetOrderBook(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember demand = "buys";
        RequiredMember supply = "sells";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new OrderBook
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Demand =
                demand.Map(static (in values) => values.GetList(static (in value) => value.GetOrderBookLine())
                ),
            Supply = supply.Map(static (in values) =>
                values.GetList(static (in value) => value.GetOrderBookLine())
            )
        };
    }
}
