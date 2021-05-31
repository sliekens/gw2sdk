using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Continents.Fixtures
{
    public class ContinentFixture
    {
        public ContinentFixture()
        {
            var reader = new FlatFileReader();
            Continents = reader.Read("Data/continents.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Continents { get; }
    }
}
