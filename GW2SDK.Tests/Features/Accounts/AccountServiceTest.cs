using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Exceptions;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountServiceTest
    {
        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public async Task GetAccount_WithApiKey_ShouldReturnAccount()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountService>();

            var actual = await sut.GetAccount();

            Assert.IsType<Account>(actual);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public async Task GetAccount_WithoutAccessToken_ShouldThrowUnauthorizedOperationException()
        {
            var services = new Container();
            var sut = services.Resolve<AccountService>();
            await Assert.ThrowsAsync<UnauthorizedOperationException>(async () => await sut.GetAccount());
        }
    }
}
