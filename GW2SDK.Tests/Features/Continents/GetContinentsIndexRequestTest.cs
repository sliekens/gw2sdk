using System;
using System.Net.Http;
using GW2SDK.Continents.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    public class GetContinentsIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetContinentsIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Continents()
        {
            var sut = new GetContinentsIndexRequest();

            var expected = new Uri("/v2/continents", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
