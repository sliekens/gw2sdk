using GW2SDK.Currencies;
using GW2SDK.Tests.Features.Currencies.Fixture;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Currencies
{
    public class CurrencyTest : IClassFixture<CurrencyFixture>
    {
        public CurrencyTest(CurrencyFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly CurrencyFixture _fixture;

        private readonly ITestOutputHelper _output;

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
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.Currencies,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Currency>(json, settings);
                    CurrencyFact.Id_is_positive(actual);
                    CurrencyFact.Name_is_not_empty(actual);
                    CurrencyFact.Description_is_not_empty(actual);
                    CurrencyFact.Order_is_positive(actual);
                    CurrencyFact.Icon_is_not_empty(actual);
                });
        }
    }
}
