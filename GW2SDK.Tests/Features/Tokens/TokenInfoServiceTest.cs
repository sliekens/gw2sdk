using System.Threading.Tasks;
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
        public async Task It_can_get_the_token_info_for_an_api_key()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TokenInfoService>();

            var actual = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull);

            Assert.IsType<ApiKeyInfo>(actual.Value);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_the_token_info_for_a_subtoken()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TokenInfoService>();

            var actual = await sut.GetTokenInfo(_serviceFixture.SubtokenBasic.Subtoken);

            Assert.IsType<SubtokenInfo>(actual.Value);
        }
    }
}
