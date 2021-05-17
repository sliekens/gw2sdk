using System.Text.Json;
using GW2SDK.Commerce.Listings;
using GW2SDK.Json;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Listings
{
    public class ItemListingReaderTest : IClassFixture<ItemListingFixture>
    {
        public ItemListingReaderTest(ItemListingFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemListingFixture _fixture;

        private static class ItemListingFact
        {
            public static void Id_is_positive(ItemListing actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature", "Commerce.Listings")]
        [Trait("Category", "Integration")]
        public void Item_listings_can_be_created_from_json()
        {
            var sut = new ItemListingReader();

            AssertEx.ForEach(_fixture.ItemPrices,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    ItemListingFact.Id_is_positive(actual);
                });
        }
    }
}
