using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Exchange;

public class GemsForGold
{
    [Fact]
    public async Task You_can_exchange_gems_for_gold()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int gems = 800;

        var (actual, _) = await sut.Commerce.ExchangeGemsForGold(gems);

        Assert.True(actual.CoinsToReceive > 10000, "800 gems should be worth some gold.");
        Assert.True(actual.CoinsPerGem > 0, "Gems can't be free.");
    }
}
