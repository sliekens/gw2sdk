using GW2SDK.Features.Builds;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Builds.Fixtures;
using GW2SDK.Tests.Shared;
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

        private Build CreateSut(JsonSerializerSettings jsonSerializerSettings)
        {
            var sut = new Build();
            JsonConvert.PopulateObject(_fixture.JsonBuildObject, sut, jsonSerializerSettings);
            return sut;
        }

        [Fact]
        [Trait("Feature", "Builds")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Build_ShouldHaveNoMissingMembers()
        {
            _ = CreateSut(new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());
        }

        [Fact]
        [Trait("Feature", "Builds")]
        [Trait("Category", "Integration")]
        public void Build_Id_ShouldBePositive()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.InRange(sut.Id, 1, int.MaxValue);
        }
    }
}
