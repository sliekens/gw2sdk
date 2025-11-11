using GuildWars2.Hero.Wallet;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Wallet;

[ServiceDataSource]
public class Wallet(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<CurrencyAmount> actual, _) = await sut.Hero.Wallet.GetWallet(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        CurrencyAmount coins = actual.Single(currency => currency.CurrencyId == 1);
        Coin coinsAmount = coins.Amount;
        Assert.True(coinsAmount > 0);
    }
}
