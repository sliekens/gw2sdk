using System.Threading.Tasks;
using GW2SDK.Accounts.Banks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Banks
{
    public class BankServiceTest
    {
        [Fact]
        public async Task It_can_get_the_bank()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BankService>();

            var actual = await sut.GetBank(ConfigurationManager.Instance.ApiKeyFull);

            Assert.IsType<Bank>(actual);
        }
    }
}
