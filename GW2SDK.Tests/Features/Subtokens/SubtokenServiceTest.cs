using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Subtokens;
using GW2SDK.Infrastructure.Subtokens;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenServiceTest : IClassFixture<ConfigurationFixture>
    {
        public SubtokenServiceTest(ConfigurationFixture configuration)
        {
            _configuration = configuration;
        }

        private readonly ConfigurationFixture _configuration;

        private SubtokenService CreateSut()
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
            http.UseAccessToken(_configuration.ApiKeyFull);

            var api = new SubtokenJsonService(http);
            return new SubtokenService(api);
        }

        [Fact]
        public async Task CreateSubtoken_ShouldNotBeNull()
        {
            var sut = CreateSut();

            var actual = await sut.CreateSubtoken();

            Assert.NotNull(actual);
        }
    }
}
