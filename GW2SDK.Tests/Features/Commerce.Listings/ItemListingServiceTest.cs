using System;
using System.Threading.Tasks;
using GW2SDK.Commerce.Listings;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Listings
{
    public class ItemListingServiceTest
    {
        [Fact]
        [Trait("Feature",  "Commerce.Listings")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_item_listing_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemListingService>();

            var actual = await sut.GetItemListingsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Commerce.Listings")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_an_item_listing_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemListingService>();

            const int itemId = 24;

            var actual = await sut.GetItemListingById(itemId);

            Assert.Equal(itemId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Commerce.Listings")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_item_listings_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemListingService>();

            var ids = new[] { 24, 19699, 35984 };

            var actual = await sut.GetItemListingsByIds(ids);

            Assert.Collection(actual,
                first => Assert.Equal(24,     first.Id),
                second => Assert.Equal(19699, second.Id),
                third => Assert.Equal(35984,  third.Id));
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task Item_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemListingService>();

            await Assert.ThrowsAsync<ArgumentNullException>("itemIds",
                async () =>
                {
                    await sut.GetItemListingsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task Item_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemListingService>();

            await Assert.ThrowsAsync<ArgumentException>("itemIds",
                async () =>
                {
                    await sut.GetItemListingsByIds(new int[0]);
                });
        }
    }
}
