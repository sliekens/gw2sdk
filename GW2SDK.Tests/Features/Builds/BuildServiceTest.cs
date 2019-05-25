using System.Threading.Tasks;
using GW2SDK.Features.Builds;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Builds
{
    public class BuildServiceTest : IClassFixture<ConfigurationFixture>, IClassFixture<HttpFixture>
    {
        public BuildServiceTest(HttpFixture http)
        {
            _http = http;
        }

        private readonly HttpFixture _http;

        [Fact]
        [Trait("Feature", "Builds")]
        [Trait("Category", "E2E")]
        public async Task GetBuild_ShouldNotReturnNull()
        {
            var sut = new BuildService(_http.Http);

            var actual = await sut.GetBuild();

            Assert.NotNull(actual);
        }
    }
}
