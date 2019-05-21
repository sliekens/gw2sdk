using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Builds;
using GW2SDK.Infrastructure.Builds;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
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
            var policy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3));
            var handler = new PolicyHttpMessageHandler(policy)
            {
                InnerHandler = new SocketsHttpHandler()
            };
            var http = new HttpClient(handler)
            {
                BaseAddress = _configuration.BaseAddress
            };

            http.UseLatestSchemaVersion();

            var api = new BuildJsonService(http);
            return new BuildService(api);
        }

        [Fact]
        [Trait("Feature", "Builds")]
        [Trait("Category", "E2E")]
        public async Task GetBuild_ShouldNotReturnNull()
        {
            var sut = CreateSut();

            var actual = await sut.GetBuild();

            Assert.NotNull(actual);
        }
    }
}
