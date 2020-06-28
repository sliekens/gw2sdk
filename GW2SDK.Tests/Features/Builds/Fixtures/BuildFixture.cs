using System.IO;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture
    {
        public BuildFixture()
        {
            Build = File.ReadAllText("Data/build.json");
        }

        public string Build { get; }
    }
}
