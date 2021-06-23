using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Continents
{
    public class ContinentFixture
    {
        public ContinentFixture()
        {
            var reader = new FlatFileReader();
            Floors = reader.Read("Data/continents_1_floors.json.gz")
                .Concat(reader.Read("Data/continents_2_floors.json.gz"))
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Floors { get; }
    }
}
