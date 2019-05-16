using GW2SDK.Features.Builds;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Builds.Fixtures;
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

        [Fact]
        [Trait("Feature", "Builds")]
        [Trait("Category", "Integration")]
        public void Build_ShouldHaveNoMissingMembers()
        {
            _output.WriteLine(_fixture.JsonBuild);

            var actual = new Build();

            var serializerSettings = Json.DefaultJsonSerializerSettings;
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            JsonConvert.PopulateObject(_fixture.JsonBuild, actual, serializerSettings);
        }

        [Fact]
        [Trait("Feature", "Builds")]
        [Trait("Category", "Integration")]
        public void Build_Id_ShouldBePositive()
        {
            var actual = new Build();

            JsonConvert.PopulateObject(_fixture.JsonBuild, actual, Json.DefaultJsonSerializerSettings);

            Assert.InRange(actual.Id, 1, int.MaxValue);
        }
    }
}
