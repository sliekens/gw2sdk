using System;
using System.Net.Http;
using GW2SDK.Items.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class GetItemsIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetItemsIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Items()
        {
            var sut = new GetItemsIndexRequest();

            var expected = new Uri("/v2/items", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
