using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Continents.Fixtures
{
    public class FloorFixture
    {
        public FloorFixture()
        {
            var reader = new FlatFileReader();
            Db = new InMemoryFloorDb(reader.Read("Data/continents_1_floors.json").Concat(reader.Read("Data/continents_2_floors.json")));
        }

        public InMemoryFloorDb Db { get; }
    }
}
