using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Prices;

internal static class ItemPriceJson
{
    public static ItemPrice GetItemPrice(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember whitelisted = "whitelisted";
        RequiredMember demand = "quantity";
        RequiredMember bestBid = "unit_price";
        RequiredMember supply = "quantity";
        RequiredMember bestAsk = "unit_price";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(whitelisted.Name))
            {
                whitelisted = member;
            }
            else if (member.NameEquals("buys"))
            {
                foreach (var buy in member.Value.EnumerateObject())
                {
                    if (buy.NameEquals(demand.Name))
                    {
                        demand = buy;
                    }
                    else if (buy.NameEquals(bestBid.Name))
                    {
                        bestBid = buy;
                    }
                    else if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedMember(buy.Name));
                    }
                }
            }
            else if (member.NameEquals("sells"))
            {
                foreach (var sell in member.Value.EnumerateObject())
                {
                    if (sell.NameEquals(supply.Name))
                    {
                        supply = sell;
                    }
                    else if (sell.NameEquals(bestAsk.Name))
                    {
                        bestAsk = sell;
                    }
                    else if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedMember(sell.Name));
                    }
                }
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemPrice
        {
            Id = id.Map(value => value.GetInt32()),
            Whitelisted = whitelisted.Map(value => value.GetBoolean()),
            TotalDemand = demand.Map(value => value.GetInt32()),
            TotalSupply = supply.Map(value => value.GetInt32()),
            BestBid = bestBid.Map(value => value.GetInt32()),
            BestAsk = bestAsk.Map(value => value.GetInt32())
        };
    }
}
