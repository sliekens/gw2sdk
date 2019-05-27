using System.Threading.Tasks;
using GW2SDK.Features.Builds;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
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
    }
}
