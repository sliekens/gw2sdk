using GW2SDK.Tests.Shared;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class WorldFixture
    {
        public WorldFixture()
        {
            var reader = new JsonFlatFileReader();

            foreach (var world in reader.Read("Data/worlds.json"))
            {
                Db.AddWorld(world);
            }
        }

        public InMemoryWorldDb Db { get; } = new InMemoryWorldDb();
    }
}
