using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Features.Tokens;
using GW2SDK.Tests.Features.Tokens.Fixtures;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens
{
    public class TokenInfoServiceTest : IClassFixture<TokenInfoServiceFixture>
    {
        public TokenInfoServiceTest(TokenInfoServiceFixture serviceFixture)
        {
            _serviceFixture = serviceFixture;
        }

        private readonly TokenInfoServiceFixture _serviceFixture;

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithApiKey_ShouldReturnApiKeyInfo()
        {
            var services = new Container();
            var sut = services.Resolve<TokenInfoService>();

            var actual = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull);

            Assert.IsType<ApiKeyInfo>(actual);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithApiKeyInDefaultRequestHeaders_ShouldReturnApiKeyInfo()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<TokenInfoService>();

            var actual = await sut.GetTokenInfo(null);

            Assert.IsType<ApiKeyInfo>(actual);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithAccessTokenNull_ShouldThrowUnauthorizedOperationException()
        {
            var services = new Container();
            var sut = services.Resolve<TokenInfoService>();

            await Assert.ThrowsAsync<UnauthorizedOperationException>(async () =>
            {
                // Next statement should throw because argument is null and HttpClient.DefaultRequestHeaders is not configured
                _ = await sut.GetTokenInfo(null);
            });
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithSubtoken_ShouldReturnSubtokenInfo()
        {
            var services = new Container();
            var sut = services.Resolve<TokenInfoService>();

            var actual = await sut.GetTokenInfo(_serviceFixture.SubtokenBasic.Subtoken);

            Assert.IsType<SubtokenInfo>(actual);
        }
    }
}
