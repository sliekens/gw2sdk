using System;
using System.Net.Http;
using GW2SDK.Commerce.Prices.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Prices
{
    public class GetItemPricesIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetItemPricesIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Unit")]
        public void RequestUri_is_v2_commerce_prices()
        {
            var sut = new GetItemPricesIndexRequest();

            var expected = new Uri("/v2/commerce/prices", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
