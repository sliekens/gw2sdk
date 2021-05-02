using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Skins.Fixtures
{
    public class SkinFixture
    {
        public SkinFixture()
        {
            var reader = new FlatFileReader();
            Db = new InMemorySkinDb(reader.Read("Data/skins.json"));
        }

        public InMemorySkinDb Db { get; }
    }
}
