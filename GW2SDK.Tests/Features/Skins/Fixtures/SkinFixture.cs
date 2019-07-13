using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Skins.Fixtures
{
    public class SkinFixture
    {
        public SkinFixture()
        {
            var reader = new JsonFlatFileReader();
            Db = new InMemorySkinDb(reader.Read("Data/skins.json"));
        }

        public InMemorySkinDb Db { get; }
    }
}
