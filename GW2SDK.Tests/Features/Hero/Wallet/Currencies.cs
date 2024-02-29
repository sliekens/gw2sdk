using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Wallet;

public class Currencies
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Wallet.GetCurrencies();

        Assert.Equal(actual.Count, context.ResultTotal);
        Assert.All(
            actual,
            currency =>
            {
                currency.Id_is_positive();
                if (currency.Id == 74)
                {
                    // Unknown currency
                    Assert.Empty(currency.Name);
                    Assert.Empty(currency.Description);
                }
                else
                {
                    currency.Name_is_not_empty();
                    currency.Description_is_not_empty();
                }

                currency.Order_is_positive();
                currency.Icon_is_not_empty();
            }
        );
    }
}
