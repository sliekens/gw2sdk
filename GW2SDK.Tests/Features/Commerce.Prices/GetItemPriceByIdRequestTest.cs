using System;
using System.Net.Http;
using GW2SDK.Commerce.Prices.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Prices
{
    public class GetItemPriceByIdRequestTest
    {
        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var id = 1;

            var actual = new GetItemPriceByIdRequest.Builder(id).GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_id()
        {
            var id = 1;

            var actual = new GetItemPriceByIdRequest.Builder(id).GetRequest();

            var expected = new Uri("/v2/commerce/prices?id=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
