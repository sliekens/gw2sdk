using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts;
using GW2SDK.Features.Accounts.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountServiceTest : IClassFixture<ConfigurationFixture>
    {
        public AccountServiceTest(ConfigurationFixture configuration)
        {
            _configuration = configuration;
        }

        private readonly ConfigurationFixture _configuration;

        private AccountService CreateSut()
        {
            var http = new HttpClient()
                .WithBaseAddress(_configuration.BaseAddress)
                .WithAccessToken(_configuration.ApiKey)
                .WithLatestSchemaVersion();

            var api = new JsonAccountsService(http);
            return new AccountService(api);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task GetAccount_ShouldNotReturnNull()
        {
            var sut = CreateSut();

            var actual = await sut.GetAccount();

            Assert.NotNull(actual);
        }
    }
}
