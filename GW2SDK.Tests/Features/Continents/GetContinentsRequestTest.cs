using System;
using System.Net.Http;
using GW2SDK.Continents.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    public class GetContinentsRequestTest
    {
        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetContinentsRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2ContinentsBulk()
        {
            var sut = new GetContinentsRequest();

            var expected = new Uri("/v2/continents?ids=all", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
