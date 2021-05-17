using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ItemListingLine
    {
        public int Listings { get; init; }

        public int Quantity { get; init; }

        public int UnitPrice { get; init; }
    }
}
