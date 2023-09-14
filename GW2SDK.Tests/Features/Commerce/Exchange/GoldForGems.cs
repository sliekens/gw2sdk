using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Exchange;

public class GoldForGems
{
    [Fact]
    public async Task You_can_exchange_gold_for_gems()
    {
        var sut = Composer.Resolve<Gw2Client>();

        Coin coins = new(100, 0, 0);

        var actual = await sut.Commerce.ExchangeGoldForGems(coins);

        Assert.True(actual.Value.GemsToReceive > 0, "100 gold should be worth some gems.");
        Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
    }
}
