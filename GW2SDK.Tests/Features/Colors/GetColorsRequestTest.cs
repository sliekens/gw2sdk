using System;
using System.Net.Http;
using GW2SDK.Colors.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    public class GetColorsRequestTest
    {
        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetColorsRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void RequestUri_is_v2_colors_bulk()
        {
            var sut = new GetColorsRequest();

            var expected = new Uri("/v2/colors?ids=all", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
