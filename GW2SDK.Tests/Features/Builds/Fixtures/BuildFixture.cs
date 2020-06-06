using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture
    {
        public BuildFixture()
        {
            var reader = new JsonFlatFileReader();
            Build = reader.Read("Data/build.json").Single();
        }

        public string Build { get; }
    }
}
