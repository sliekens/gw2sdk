using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Prices;

[PublicAPI]
public static class ItemPriceJson
{
    public static ItemPrice GetItemPrice(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<bool> whitelisted = new("whitelisted");
        RequiredMember<int> demand = new("quantity");
        RequiredMember<int> bestBid = new("unit_price");
        RequiredMember<int> supply = new("quantity");
        RequiredMember<int> bestAsk = new("unit_price");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(whitelisted.Name))
            {
                whitelisted.Value = member.Value;
            }
            else if (member.NameEquals("buys"))
            {
                foreach (var buy in member.Value.EnumerateObject())
                {
                    if (buy.NameEquals(demand.Name))
                    {
                        demand.Value = buy.Value;
                    }
                    else if (buy.NameEquals(bestBid.Name))
                    {
                        bestBid.Value = buy.Value;
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
                        supply.Value = sell.Value;
                    }
                    else if (sell.NameEquals(bestAsk.Name))
                    {
                        bestAsk.Value = sell.Value;
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
            Id = id.GetValue(),
            Whitelisted = whitelisted.GetValue(),
            TotalDemand = demand.GetValue(),
            TotalSupply = supply.GetValue(),
            BestBid = bestBid.GetValue(),
            BestAsk = bestAsk.GetValue()
        };
    }
}
