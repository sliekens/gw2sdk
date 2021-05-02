using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Titles.Fixtures
{
    public class TitlesFixture
    {
        public TitlesFixture()
        {
            var reader = new FlatFileReader();
            Titles = reader.Read("Data/titles.json").ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Titles { get; }
    }
}
