using System.Threading.Tasks;
using GW2SDK.Features.Accounts;
using GW2SDK.Infrastructure.Accounts;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountServiceTest : IClassFixture<HttpFixture>
    {
        public AccountServiceTest(HttpFixture http)
        {
            _http = http;
        }

        private readonly HttpFixture _http;

        private AccountService CreateSut()
        {
            var api = new AccountJsonService(_http.HttpFullAccess);
            return new AccountService(api);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "E2E")]
        public async Task GetAccount_ShouldNotReturnNull()
        {
            var sut = CreateSut();

            var actual = await sut.GetAccount();

            Assert.NotNull(actual);
        }
    }
}
