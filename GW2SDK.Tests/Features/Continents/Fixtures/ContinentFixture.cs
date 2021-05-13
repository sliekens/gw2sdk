using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Continents.Fixtures
{
    public class ContinentFixture
    {
        public ContinentFixture()
        {
            var reader = new FlatFileReader();
            Db = new InMemoryContinentDb(reader.Read("Data/continents.json"));
        }

        public InMemoryContinentDb Db { get; }
    }
}
