using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Commerce.Prices.Fixtures
{
    public class ItemPriceFixture
    {
        public ItemPriceFixture()
        {
            var reader = new JsonFlatFileReader();
            Db = new InMemoryItemPriceDb(reader.Read("Data/prices.json"));
        }

        public InMemoryItemPriceDb Db { get; }
    }
}
