using System.Threading.Tasks;
using GW2SDK.Exceptions;
using GW2SDK.Tests.Features.Tokens.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens;
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
        public async Task Get_token_info_for_api_key()
        {
            await using var services = new Container();
            var sut = services.Resolve<TokenInfoService>();

            var actual = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull);

            Assert.IsType<ApiKeyInfo>(actual);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task Get_token_info_for_default_authorization()
        {
            await using var services = new Container(ConfigurationManager.Instance.ApiKeyFull);

            var sut = services.Resolve<TokenInfoService>();

            var actual = await sut.GetTokenInfo(null);

            Assert.IsType<ApiKeyInfo>(actual);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task Access_token_cannot_be_null()
        {
            await using var services = new Container();
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
        public async Task Get_token_info_for_subtoken()
        {
            await using var services = new Container();
            var sut = services.Resolve<TokenInfoService>();

            var actual = await sut.GetTokenInfo(_serviceFixture.SubtokenBasic.Subtoken);

            Assert.IsType<SubtokenInfo>(actual);
        }
    }
}
