using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Items.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class GetItemsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetItemsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetItemsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/items?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Item_ids_cannot_be_null()
        {
            Assert.Throws<ArgumentNullException>("itemIds",
                () =>
                {
                    _ = new GetItemsByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Item_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("itemIds",
                () =>
                {
                    _ = new GetItemsByIdsRequest.Builder(new int[0]);
                });
        }
    }
}
