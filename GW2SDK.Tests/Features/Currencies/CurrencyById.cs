using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Currencies;

public class CurrencyById
{
    [Fact]
    public async Task A_currency_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Wallet.GetCurrencyById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
