using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Currencies;

public class CurrenciesByPage
{
    [Fact]
    public async Task Currencies_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wallet.GetCurrenciesByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.NotNull(actual.Context.PageContext);
        Assert.Equal(3, actual.Context.PageContext.PageSize);
    }
}
