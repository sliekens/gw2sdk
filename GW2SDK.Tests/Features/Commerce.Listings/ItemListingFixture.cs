using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Commerce.Listings
{
    public class ItemListingFixture
    {
        public ItemListingFixture()
        {
            var reader = new FlatFileReader();
            ItemPrices = reader.Read("Data/listings.json")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> ItemPrices { get; }
    }
}
