using System.Text.Json;
using GW2SDK.Currencies;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Currencies.Fixture;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Currencies
{
    public class CurrencyReaderTest : IClassFixture<CurrencyFixture>
    {
        public CurrencyReaderTest(CurrencyFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly CurrencyFixture _fixture;

        private static class CurrencyFact
        {
            public static void Id_is_positive(Currency actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Name_is_not_empty(Currency actual) => Assert.NotEmpty(actual.Name);

            public static void Description_is_not_empty(Currency actual) => Assert.NotEmpty(actual.Description);

            public static void Order_is_positive(Currency actual) => Assert.InRange(actual.Order, 1, 1000);

            public static void Icon_is_not_empty(Currency actual) => Assert.NotEmpty(actual.Icon);
        }

        [Fact]
        [Trait("Feature",    "Currencies")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Currencies_can_be_created_from_json()
        {
            var sut = new CurrencyReader();

            AssertEx.ForEach(_fixture.Currencies,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    CurrencyFact.Id_is_positive(actual);
                    CurrencyFact.Name_is_not_empty(actual);
                    CurrencyFact.Description_is_not_empty(actual);
                    CurrencyFact.Order_is_positive(actual);
                    CurrencyFact.Icon_is_not_empty(actual);
                });
        }
    }
}
