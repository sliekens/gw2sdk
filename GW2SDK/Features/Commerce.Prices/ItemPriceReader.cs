﻿using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    public sealed class ItemPriceReader : IItemPriceReader
    {
        public ItemPrice Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var whitelisted = new RequiredMember<bool>("whitelisted");
            var demand = new RequiredMember<int>("quantity");
            var bestBid = new RequiredMember<int>("unit_price");
            var supply = new RequiredMember<int>("quantity");
            var bestAsk = new RequiredMember<int>("unit_price");

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

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
