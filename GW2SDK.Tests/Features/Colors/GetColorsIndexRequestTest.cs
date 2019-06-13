using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Colors;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    public class GetColorsIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetColorsIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Colors()
        {
            var sut = new GetColorsIndexRequest();

            var expected = new Uri("/v2/colors", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
