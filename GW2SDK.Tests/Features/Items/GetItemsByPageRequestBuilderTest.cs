using System;
using System.Net.Http;
using GW2SDK.Items.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class GetItemsByPageRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetItemsByPageRequest.Builder(0);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_page()
        {
            var sut = new GetItemsByPageRequest.Builder(1);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/items?page=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_page_size()
        {
            var sut = new GetItemsByPageRequest.Builder(1, 200);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/items?page=1&page_size=200", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
