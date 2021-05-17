using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    public interface IItemListingReader : IJsonReader<ItemListing>
    {
        IJsonReader<int> Id { get; }
    }
}