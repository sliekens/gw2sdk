using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountServiceTest
    {
        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_the_account()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountService>();

            var actual = await sut.GetAccount(ConfigurationManager.Instance.ApiKeyFull);

            Assert.IsType<Account>(actual);
        }
    }
}
