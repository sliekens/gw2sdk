using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Commerce.Prices.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Prices
{
    public class GetItemPricesByIdsRequestTest
    {
        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetItemPricesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetItemPricesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/commerce/prices?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Unit")]
        public void Item_ids_cannot_be_null()
        {
            Assert.Throws<ArgumentNullException>("itemIds",
                () =>
                {
                    _ = new GetItemPricesByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Unit")]
        public void Item_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("itemIds",
                () =>
                {
                    _ = new GetItemPricesByIdsRequest.Builder(new int[0]);
                });
        }
    }
}
