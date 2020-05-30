using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Commerce.Prices.Fixtures
{
    public class InMemoryItemPriceDb
    {
        public InMemoryItemPriceDb(IEnumerable<string> objects)
        {
            ItemPrices = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> ItemPrices { get; }
    }
}
