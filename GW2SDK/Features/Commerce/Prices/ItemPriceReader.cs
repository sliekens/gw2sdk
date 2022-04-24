using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Prices;

[PublicAPI]
public static class ItemPriceReader
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
                id = id.From(member.Value);
            }
            else if (member.NameEquals(whitelisted.Name))
            {
                whitelisted = whitelisted.From(member.Value);
            }
            else if (member.NameEquals("buys"))
            {
                foreach (var buy in member.Value.EnumerateObject())
                {
                    if (buy.NameEquals(demand.Name))
                    {
                        demand = demand.From(buy.Value);
                    }
                    else if (buy.NameEquals(bestBid.Name))
                    {
                        bestBid = bestBid.From(buy.Value);
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
                        supply = supply.From(sell.Value);
                    }
                    else if (sell.NameEquals(bestAsk.Name))
                    {
                        bestAsk = bestAsk.From(sell.Value);
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
