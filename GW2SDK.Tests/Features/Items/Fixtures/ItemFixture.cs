using GW2SDK.Tests.Shared;

namespace GW2SDK.Tests.Features.Items.Fixtures
{
    public class ItemFixture
    {
        public ItemFixture()
        {
            var reader = new JsonFlatFileReader();

            foreach (var item in reader.Read("Data/items.json"))
            {
                Db.AddItem(item);
            }
        }

        public InMemoryItemDb Db { get; } = new InMemoryItemDb();
    }
}
