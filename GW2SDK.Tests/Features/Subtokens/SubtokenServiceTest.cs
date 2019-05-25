using System.Threading.Tasks;
using GW2SDK.Features.Subtokens;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenServiceTest : IClassFixture<HttpFixture>
    {
        public SubtokenServiceTest(HttpFixture http)
        {
            _http = http;
        }

        private readonly HttpFixture _http;

        [Fact]
        public async Task CreateSubtoken_ShouldNotBeNull()
        {
            var sut = new SubtokenService(_http.HttpFullAccess);

            var actual = await sut.CreateSubtoken();

            Assert.NotNull(actual);
        }
    }
}
