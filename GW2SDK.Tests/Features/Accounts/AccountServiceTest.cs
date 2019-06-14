using System.Threading.Tasks;
using GW2SDK.Features.Accounts;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountServiceTest
    {
        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public async Task GetAccount_ShouldReturnAccount()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountService>();

            var actual = await sut.GetAccount();

            Assert.IsType<Account>(actual);
        }
    }
}
