using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Commerce.Prices.Fixtures
{
    public class ItemPriceFixture
    {
        public ItemPriceFixture()
        {
            var reader = new FlatFileReader();
            ItemPrices = reader.Read("Data/prices.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> ItemPrices { get; }
    }
}
