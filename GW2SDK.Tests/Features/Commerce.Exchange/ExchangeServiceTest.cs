using System.Threading.Tasks;
using GW2SDK.Commerce.Exchange;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Exchange
{
    public class ExchangeServiceTest
    {
        [Fact]
        public async Task It_can_calculate_gold_for_gems()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ExchangeService>();

            var coins = 100_00_00; // 100 gold

            var actual = await sut.ExchangeGoldForGems(coins);

            Assert.True(actual.Value.GemsToReceive > 0, "100 gold should be worth some gems.");
            Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
        }

        [Fact]
        public async Task It_can_calculate_gems_for_gold()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ExchangeService>();

            var gems = 800;

            var actual = await sut.ExchangeGemsForGold(gems);

            Assert.True(actual.Value.CoinsToReceive > 10000, "800 gems should be worth some gold.");
            Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
        }
    }
}
