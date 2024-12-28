using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Exchange;

public class ExchangeGemsToGold
{
    [Fact]
    public async Task You_can_exchange_gems_for_gold()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int gems = 800;

        var (actual, context) = await sut.Commerce.ExchangeGemsToGold(
            gems,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.True(actual.Gold > 10000, "800 gems should be worth some gold.");
        Assert.True(actual.ExchangeRate > 0, "Gems can't be free.");
    }
}
