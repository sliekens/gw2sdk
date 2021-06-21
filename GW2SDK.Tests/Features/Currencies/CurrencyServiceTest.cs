using System;
using System.Threading.Tasks;
using GW2SDK.Currencies;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Currencies
{
    public class CurrencyServiceTest
    {
        private static class CurrencyFact
        {
            public static void Id_is_positive(Currency actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Name_is_not_empty(Currency actual) => Assert.NotEmpty(actual.Name);

            public static void Description_is_not_empty(Currency actual) => Assert.NotEmpty(actual.Description);

            public static void Order_is_positive(Currency actual) => Assert.InRange(actual.Order, 1, 1000);

            public static void Icon_is_not_empty(Currency actual) => Assert.NotEmpty(actual.Icon);
        }


        [Fact]
        [Trait("Feature",  "Currencies")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_currencies()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CurrencyService>();

            var actual = await sut.GetCurrencies();

            Assert.Equal(actual.ResultTotal, actual.Count);
            Assert.All(actual,
                currency =>
                {
                    CurrencyFact.Id_is_positive(currency);
                    CurrencyFact.Name_is_not_empty(currency);
                    CurrencyFact.Description_is_not_empty(currency);
                    CurrencyFact.Order_is_positive(currency);
                    CurrencyFact.Icon_is_not_empty(currency);
                });
        }

        [Fact]
        [Trait("Feature",  "Currencies")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_currency_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CurrencyService>();

            var actual = await sut.GetCurrenciesIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Currencies")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_currency_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CurrencyService>();

            const int currencyId = 1;

            var actual = await sut.GetCurrencyById(currencyId);

            Assert.Equal(currencyId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Currencies")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_currencies_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CurrencyService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetCurrenciesByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "Currencies")]
        [Trait("Category", "Unit")]
        public async Task Currency_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CurrencyService>();

            await Assert.ThrowsAsync<ArgumentNullException>("currencyIds",
                async () =>
                {
                    await sut.GetCurrenciesByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Currencies")]
        [Trait("Category", "Unit")]
        public async Task Currency_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CurrencyService>();

            await Assert.ThrowsAsync<ArgumentException>("currencyIds",
                async () =>
                {
                    await sut.GetCurrenciesByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Currencies")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_currencies_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CurrencyService>();

            var actual = await sut.GetCurrenciesByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
