using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record ItemListing
    {
        public int Id { get; init; }

        public ItemListingLine[] Demand { get; init; } = new ItemListingLine[0];

        public ItemListingLine[] Supply { get; init; } = new ItemListingLine[0];
    }
}
