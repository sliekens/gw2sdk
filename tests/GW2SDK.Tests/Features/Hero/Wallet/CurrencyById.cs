using GuildWars2.Hero.Wallet;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Wallet;

[ServiceDataSource]
public class CurrencyById(Gw2Client sut)
{
    [Test]
    public async Task A_currency_can_be_found_by_id()
    {
        const int id = 1;
        (Currency actual, MessageContext context) = await sut.Hero.Wallet.GetCurrencyById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
