using System.Threading.Tasks;
using GW2SDK.Features.Builds;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Builds
{
    public class BuildServiceTest
    {
        public BuildServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Builds")]
        [Trait("Category", "Integration")]
        public async Task GetBuild_ShouldReturnBuild()
        {
            var http = HttpClientFactory.CreateDefault();
            var sut = new BuildService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetBuild(settings);

            Assert.IsType<Build>(actual);
        }

        [Fact]
        [Trait("Feature",    "Builds")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public async Task Build_ShouldHaveNoMissingMembers()
        {
            var http = HttpClientFactory.CreateDefault();
            var sut = new BuildService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            // Next statement throws if there are missing members
            _ = await sut.GetBuild(settings);
        }

        [Fact]
        [Trait("Feature",  "Builds")]
        [Trait("Category", "Integration")]
        public async Task Build_Id_ShouldBePositive()
        {
            var http = HttpClientFactory.CreateDefault();
            var sut = new BuildService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetBuild(settings);

            Assert.InRange(actual.Id, 1, int.MaxValue);
        }
    }
}
