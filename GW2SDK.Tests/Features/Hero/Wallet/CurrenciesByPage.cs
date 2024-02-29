using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Wallet;

public class CurrenciesByPage
{
    [Fact]
    public async Task Currencies_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Wallet.GetCurrenciesByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, context.PageSize);
    }
}
