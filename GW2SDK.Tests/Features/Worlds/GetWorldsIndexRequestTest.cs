using System;
using System.Net.Http;
using GW2SDK.Worlds.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class GetWorldsIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetWorldsIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Worlds()
        {
            var sut = new GetWorldsIndexRequest();

            var expected = new Uri("/v2/worlds", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
