using GuildWars2.Hero.Wallet;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Wallet;

public class Wallet
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<CurrencyAmount> actual, _) = await sut.Hero.Wallet.GetWallet(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );
        CurrencyAmount coins = actual.Single(currency => currency.CurrencyId == 1);
        Coin coinsAmount = coins.Amount;
        Assert.True(coinsAmount > 0);
    }
}
