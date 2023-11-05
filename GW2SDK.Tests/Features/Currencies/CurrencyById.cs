using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Currencies;

public class CurrencyById
{
    [Fact]
    public async Task A_currency_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Wallet.GetCurrencyById(id);

        Assert.Equal(id, actual.Id);
    }
}
