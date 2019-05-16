using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Builds;
using GW2SDK.Features.Builds.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Builds
{
    public class BuildServiceTest : IClassFixture<ConfigurationFixture>
    {
        public BuildServiceTest(ConfigurationFixture configuration)
        {
            _configuration = configuration;
        }

        private readonly ConfigurationFixture _configuration;

        private BuildService CreateSut()
        {
            var http = new HttpClient()
                .WithBaseAddress(_configuration.BaseAddress)
                .WithLatestSchemaVersion();

            var api = new JsonBuildService(http);
            return new BuildService(api);
        }

        [Fact]
        [Trait("Feature", "Builds")]
        [Trait("Category", "Integration")]
        public async Task GetBuild_ShouldNotReturnNull()
        {
            var sut = CreateSut();

            var actual = await sut.GetBuild();

            Assert.NotNull(actual);
        }
    }
}
