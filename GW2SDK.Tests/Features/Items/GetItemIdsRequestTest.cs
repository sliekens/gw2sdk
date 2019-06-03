using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Items;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class GetItemIdsRequestTest
    {
        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetItemIdsRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Items()
        {
            var sut = new GetItemIdsRequest();

            var expected = new Uri("/v2/items", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
