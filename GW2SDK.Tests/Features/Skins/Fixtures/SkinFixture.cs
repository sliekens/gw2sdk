using GW2SDK.Tests.Shared;

namespace GW2SDK.Tests.Features.Skins.Fixtures
{
    public class SkinFixture
    {
        public SkinFixture()
        {
            var reader = new JsonFlatFileReader();

            foreach (var item in reader.Read("Data/skins.json"))
            {
                Db.AddSkin(item);
            }
        }

        public InMemorySkinDb Db { get; } = new InMemorySkinDb();
    }
}
