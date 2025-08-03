using GuildWars2.Hero.Wallet;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Wallet;

public class Currencies
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Currency> actual, MessageContext context) = await sut.Hero.Wallet.GetCurrencies(
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            currency =>
            {
                Assert.True(currency.Id >= 1);
                if (currency.Id == 74)
                {
                    // Unknown currency
                    Assert.Empty(currency.Name);
                    Assert.Empty(currency.Description);
                }
                else
                {
                    Assert.NotEmpty(currency.Name);
                    Assert.NotEmpty(currency.Description);
                }

                Assert.InRange(currency.Order, 1, 1000);
                Assert.True(currency.IconUrl is null || currency.IconUrl.IsAbsoluteUri);
            }
        );
    }
}
