using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Wallet;

public class Wallet
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;
        var (actual, _) = await sut.Hero.Wallet.GetWallet(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );
        var coins = actual.Single(currency => currency.CurrencyId == 1);
        Coin coinsAmount = coins.Amount;
        Assert.True(coinsAmount > 0);
    }
}
