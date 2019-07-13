using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Items.Fixtures
{
    public class ItemFixture
    {
        public ItemFixture()
        {
            var reader = new JsonFlatFileReader();
            Db = new InMemoryItemDb(reader.Read("Data/items.json"));
        }

        public InMemoryItemDb Db { get; }
    }
}
