using GW2SDK.Commerce.Prices;
using GW2SDK.Impl.JsonConverters;
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

        [Fact]
        [Trait("Feature",    "Commerce.Prices")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Item_prices_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();
            AssertEx.ForEach(_fixture.Db.ItemPrices,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<ItemPrice>(json, settings);
                });
        }
    }
}
