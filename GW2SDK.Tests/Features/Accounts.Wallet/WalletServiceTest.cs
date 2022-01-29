using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Accounts.Wallet;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Wallet
{
    public class WalletServiceTest
    {
        [Fact]
        public async Task It_can_get_your_wallet()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WalletService>();
            var accessToken = services.Resolve<ApiKeyFull>();

            var actual = await sut.GetWallet(accessToken.Key);

            Assert.True(actual.HasValue);

            var coins = actual.Value.Single(currency => currency.CurrencyId == 1);
            var coinsAmount = (Coin)coins.Amount;
            Assert.True(coinsAmount > 0);
        }
    }
}
