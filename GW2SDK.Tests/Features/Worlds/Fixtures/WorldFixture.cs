using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class WorldFixture
    {
        public WorldFixture()
        {
            var reader = new FlatFileReader();
            Db = new InMemoryWorldDb(reader.Read("Data/worlds.json"));
        }

        public InMemoryWorldDb Db { get; }
    }
}
