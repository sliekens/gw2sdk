using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Tokens;
using GW2SDK.Features.Tokens.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Tokens
{
    public class TokenInfoServiceTest : IClassFixture<ConfigurationFixture>
    {
        public TokenInfoServiceTest(ConfigurationFixture configuration, ITestOutputHelper output)
        {
            _configuration = configuration;
            _output = output;
        }

        private readonly ConfigurationFixture _configuration;

        private readonly ITestOutputHelper _output;

        private TokenInfoService CreateSut()
        {
            var http = new HttpClient()
                .WithBaseAddress(_configuration.BaseAddress)
                .WithAccessToken(_configuration.ApiKey)
                .WithLatestSchemaVersion();

            var api = new JsonTokenInfoService(http);
            return new TokenInfoService(api);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_ShouldNotReturnNull()
        {
            var sut = CreateSut();

            var actual = await sut.GetTokenInfo();

            Assert.NotNull(actual);
        }
    }
}
