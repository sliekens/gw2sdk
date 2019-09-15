using System;
using System.Net.Http;
using GW2SDK.Colors.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    public class GetColorByIdRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var id = 1;

            var sut = new GetColorByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_id()
        {
            var id = 1;

            var sut = new GetColorByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/colors?id=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
