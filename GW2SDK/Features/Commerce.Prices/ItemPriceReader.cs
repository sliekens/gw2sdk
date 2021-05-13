using System;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    public sealed class ItemPriceReader : IItemPriceReader
    {
        public ItemPrice Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var id = new RequiredMember<int>("id");
            var whitelisted = new RequiredMember<bool>("whitelisted");
            var buys = new RequiredMember<ItemBuyers>("buys");
            var sells = new RequiredMember<ItemSellers>("sells");

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
                else if (member.NameEquals(buys.Name))
                {
                    buys = buys.From(member.Value);
                }
                else if (member.NameEquals(sells.Name))
                {
                    sells = sells.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ItemPrice
            {
                Id = id.GetValue(),
                Whitelisted = whitelisted.GetValue(),
                Buyers = buys.Select(value => ReadItemBuyers(value, missingMemberBehavior)),
                Sellers = sells.Select(value => ReadItemSellers(value, missingMemberBehavior))
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        private ItemSellers ReadItemSellers(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var quantity = new RequiredMember<int>("quantity");
            var unitPrice = new RequiredMember<int>("unit_price");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(quantity.Name))
                {
                    quantity = quantity.From(member.Value);
                }
                else if (member.NameEquals(unitPrice.Name))
                {
                    unitPrice = unitPrice.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ItemSellers
            {
                OpenSellOrders = quantity.GetValue(),
                BestAsk = unitPrice.GetValue()
            };
        }

        private ItemBuyers ReadItemBuyers(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var quantity = new RequiredMember<int>("quantity");
            var unitPrice = new RequiredMember<int>("unit_price");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(quantity.Name))
                {
                    quantity = quantity.From(member.Value);
                }
                else if (member.NameEquals(unitPrice.Name))
                {
                    unitPrice = unitPrice.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ItemBuyers
            {
                OpenBuyOrders = quantity.GetValue(),
                BestBid = unitPrice.GetValue()
            };
        }
    }
}
