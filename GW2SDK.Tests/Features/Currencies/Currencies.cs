using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Currencies;

public class Currencies
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wallet.GetCurrencies();

        Assert.NotNull(context.ResultContext);
        Assert.Equal(context.ResultContext.ResultTotal, actual.Count);
        Assert.All(
            actual,
            currency =>
            {
                currency.Id_is_positive();
                currency.Name_is_not_empty();
                if (currency.Id == 63)
                {
                    // Astral Acclaim is missing a tooltip
                    Assert.Empty(currency.Description);
                }
                else
                {
                    currency.Description_is_not_empty();
                }

                currency.Order_is_positive();
                currency.Icon_is_not_empty();
            }
        );
    }
}
