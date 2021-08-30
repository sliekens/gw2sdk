using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Continents
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ContinentFixture
    {
        public ContinentFixture()
        {
            Floors = FlatFileReader.Read("Data/continents_1_floors.json.gz")
                .Concat(FlatFileReader.Read("Data/continents_2_floors.json.gz"))
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Floors { get; }
    }
}
