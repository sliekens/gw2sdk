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
            _output.WriteLine(_fixture.JsonBuildObject);

            var sut = new Build();

            var serializerSettings = Json.DefaultJsonSerializerSettings;
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            JsonConvert.PopulateObject(_fixture.JsonBuildObject, sut, serializerSettings);
        }

        [Fact]
        [Trait("Feature", "Builds")]
        [Trait("Category", "Integration")]
        public void Build_Id_ShouldBePositive()
        {
            var sut = new Build();

            JsonConvert.PopulateObject(_fixture.JsonBuildObject, sut, Json.DefaultJsonSerializerSettings);

            Assert.InRange(sut.Id, 1, int.MaxValue);
        }
    }
}
