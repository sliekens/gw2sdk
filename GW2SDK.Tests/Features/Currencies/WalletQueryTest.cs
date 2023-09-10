using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Currencies;

public class WalletQueryTest
{
    [Fact]
    public async Task Wallet_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();
        var actual = await sut.Wallet.GetWallet(accessToken.Key);
        var coins = actual.Value.Single(currency => currency.CurrencyId == 1);
        Coin coinsAmount = coins.Amount;
        Assert.True(coinsAmount > 0);
    }

    [Fact]
    public async Task Currencies_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wallet.GetCurrencies();

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

    [Fact]
    public async Task Currencies_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wallet.GetCurrenciesIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_currency_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Wallet.GetCurrencyById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Currencies_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Wallet.GetCurrenciesByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Currencies_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wallet.GetCurrenciesByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
