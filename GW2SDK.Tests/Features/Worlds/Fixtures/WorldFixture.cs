using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class WorldFixture
    {
        public WorldFixture()
        {
            var reader = new FlatFileReader();
            Worlds = reader.Read("Data/worlds.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Worlds { get; }
    }
}
