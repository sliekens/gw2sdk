using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Tokens.Fixtures;
using GW2SDK.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Tokens
{
    public class TokenInfoServiceTest : IClassFixture<TokenInfoServiceFixture>
    {
        public TokenInfoServiceTest(TokenInfoServiceFixture serviceFixture, ITestOutputHelper output)
        {
            _serviceFixture = serviceFixture;
            _output = output;
        }

        private readonly TokenInfoServiceFixture _serviceFixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithApiKey_ShouldReturnApiKeyInfo()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull, settings);

            Assert.IsType<ApiKeyInfo>(actual);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithApiKeyInDefaultRequestHeaders_ShouldReturnApiKeyInfo()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetTokenInfo(null, settings);

            Assert.IsType<ApiKeyInfo>(actual);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithAccessTokenNull_ShouldThrowUnauthorizedOperationException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            await Assert.ThrowsAsync<UnauthorizedOperationException>(async () =>
            {
                // Next statement should throw because argument is null and HttpClient.DefaultRequestHeaders is not configured
                _ = await sut.GetTokenInfo(null, settings);
            });
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithSubtoken_ShouldReturnSubtokenInfo()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetTokenInfo(_serviceFixture.SubtokenBasic.Subtoken, settings);

            Assert.IsType<SubtokenInfo>(actual);
        }
    }
}
