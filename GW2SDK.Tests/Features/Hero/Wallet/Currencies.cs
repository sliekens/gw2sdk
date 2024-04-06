using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Wallet;

public class Currencies
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Wallet.GetCurrencies();

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
                Assert.NotEmpty(currency.IconHref);
            }
        );
    }
}
