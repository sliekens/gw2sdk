using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    public sealed class ItemListingReader : IItemListingReader
    {
        public ItemListing Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var demand = new RequiredMember<ItemListingLine[]>("buys");
            var supply = new RequiredMember<ItemListingLine[]>("sells");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(demand.Name))
                {
                    demand = demand.From(member.Value);
                }
                else if (member.NameEquals(supply.Name))
                {
                    supply = supply.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new ItemListing
            {
                Id = id.GetValue(),
                Demand = demand.Select(value =>
                    value.GetArray(item => ReadItemListingLine(item, missingMemberBehavior))),
                Supply = supply.Select(value =>
                    value.GetArray(item => ReadItemListingLine(item, missingMemberBehavior)))
            };
        }

        private ItemListingLine ReadItemListingLine(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var listings = new RequiredMember<int>("listings");
            var unitPrice = new RequiredMember<int>("unit_price");
            var quantity = new RequiredMember<int>("quantity");

            foreach (var member in json.EnumerateObject())
            {
                
                if (member.NameEquals(listings.Name))
                {
                    listings = listings.From(member.Value);
                }
                else if (member.NameEquals(unitPrice.Name))
                {
                    unitPrice = unitPrice.From(member.Value);
                }
                else if (member.NameEquals(quantity.Name))
                {
                    quantity = quantity.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new ItemListingLine
            {
                Listings = listings.GetValue(),
                UnitPrice = unitPrice.GetValue(),
                Quantity = quantity.GetValue()
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}