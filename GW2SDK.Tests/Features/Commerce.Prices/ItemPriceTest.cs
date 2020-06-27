using GW2SDK.Commerce.Prices;
using GW2SDK.Tests.Features.Commerce.Prices.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Commerce.Prices
{
    [Collection(nameof(ItemPriceDbCollection))]
    public class ItemPriceTest
    {
        public ItemPriceTest(ItemPriceFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ItemPriceFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class ItemPriceFact
        {
            public static void Id_is_positive(ItemPrice actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Commerce.Prices")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Item_prices_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();
            AssertEx.ForEach(_fixture.Db.ItemPrices,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<ItemPrice>(json, settings);

                    ItemPriceFact.Id_is_positive(actual);
                });
        }
    }
}
