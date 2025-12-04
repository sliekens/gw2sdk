using GuildWars2.Commerce.Exchange;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Commerce.Exchange;

[ServiceDataSource]
public class ExchangeGemsToGold(Gw2Client sut)
{
    [Test]
    public async Task You_can_exchange_gems_for_gold()
    {
        const int gems = 800;
        (GemsToGold actual, MessageContext context) = await sut.Commerce.ExchangeGemsToGold(gems, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Gold).IsGreaterThan(10000).Because("800 gems should be worth some gold.");
        await Assert.That(actual.ExchangeRate).IsGreaterThan(0).Because("Gems can't be free.");
    }
}
