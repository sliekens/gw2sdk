using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Currencies;

public class Currencies
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wallet.GetCurrencies();

        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
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
