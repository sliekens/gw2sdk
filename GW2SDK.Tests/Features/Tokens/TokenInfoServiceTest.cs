using System.Threading.Tasks;
using GW2SDK.Features.Tokens;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Tokens
{
    public class TokenInfoServiceTest : IClassFixture<HttpFixture>
    {
        public TokenInfoServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        private TokenInfoService CreateSut() => new TokenInfoService(_http.HttpFullAccess);

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "E2E")]
        public async Task GetTokenInfo_ShouldNotReturnNull()
        {
            var sut = CreateSut();

            var actual = await sut.GetTokenInfo();

            Assert.NotNull(actual);
        }
    }
}
