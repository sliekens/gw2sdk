using GW2SDK.Builds;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Builds.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Builds
{
    public class BuildTest : IClassFixture<BuildFixture>
    {
        public BuildTest(BuildFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly BuildFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class BuildFact
        {
            public static void Id_is_positive(Build actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Builds")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Build_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();

            var actual = JsonConvert.DeserializeObject<Build>(_fixture.Build, settings);

            BuildFact.Id_is_positive(actual);
        }
    }
}
