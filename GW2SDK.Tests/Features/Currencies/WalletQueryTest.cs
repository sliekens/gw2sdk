using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Currencies;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Currencies;

public class WalletQueryTest
{
    private static class CurrencyFact
    {
        public static void Id_is_positive(Currency actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Name_is_not_empty(Currency actual) => Assert.NotEmpty(actual.Name);

        public static void Description_is_not_empty(Currency actual) =>
            Assert.NotEmpty(actual.Description);

        public static void Order_is_positive(Currency actual) =>
            Assert.InRange(actual.Order, 1, 1000);

        public static void Icon_is_not_empty(Currency actual) => Assert.NotEmpty(actual.Icon);
    }

    [Fact]
    public async Task Wallet_can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKey>();
        var actual = await sut.Wallet.GetWallet(accessToken.Key);
        var coins = actual.Value.Single(currency => currency.CurrencyId == 1);
        Coin coinsAmount = coins.Amount;
        Assert.True(coinsAmount > 0);
    }

    [Fact]
    public async Task Currencies_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Wallet.GetCurrencies();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            currency =>
            {
                CurrencyFact.Id_is_positive(currency);
                CurrencyFact.Name_is_not_empty(currency);
                CurrencyFact.Description_is_not_empty(currency);
                CurrencyFact.Order_is_positive(currency);
                CurrencyFact.Icon_is_not_empty(currency);
            }
            );
    }

    [Fact]
    public async Task Currencies_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Wallet.GetCurrenciesIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_currency_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int currencyId = 1;

        var actual = await sut.Wallet.GetCurrencyById(currencyId);

        Assert.Equal(currencyId, actual.Value.Id);
    }

    [Fact]
    public async Task Currencies_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Wallet.GetCurrenciesByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
            );
    }

    [Fact]
    public async Task Currencies_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Wallet.GetCurrenciesByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}
