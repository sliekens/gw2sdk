using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Commerce.Prices;

internal static class ItemPriceJson
{
    public static ItemPrice GetItemPrice(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember whitelisted = "whitelisted";
        RequiredMember demand = "quantity";
        RequiredMember bestBid = "unit_price";
        RequiredMember supply = "quantity";
        RequiredMember bestAsk = "unit_price";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (whitelisted.Match(member))
            {
                whitelisted = member;
            }
            else if (member.NameEquals("buys"))
            {
                foreach (JsonProperty buy in member.Value.EnumerateObject())
                {
                    if (demand.Match(buy))
                    {
                        demand = buy;
                    }
                    else if (bestBid.Match(buy))
                    {
                        bestBid = buy;
                    }
                    else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        ThrowHelper.ThrowUnexpectedMember(buy.Name);
                    }
                }
            }
            else if (member.NameEquals("sells"))
            {
                foreach (JsonProperty sell in member.Value.EnumerateObject())
                {
                    if (supply.Match(sell))
                    {
                        supply = sell;
                    }
                    else if (bestAsk.Match(sell))
                    {
                        bestAsk = sell;
                    }
                    else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        ThrowHelper.ThrowUnexpectedMember(sell.Name);
                    }
                }
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new ItemPrice
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Whitelisted = whitelisted.Map(static (in value) => value.GetBoolean()),
            TotalDemand = demand.Map(static (in value) => value.GetInt32()),
            TotalSupply = supply.Map(static (in value) => value.GetInt32()),
            BestBid = bestBid.Map(static (in value) => value.GetInt32()),
            BestAsk = bestAsk.Map(static (in value) => value.GetInt32())
        };
    }
}
