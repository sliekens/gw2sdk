using System;
using System.Net.Http;
using GW2SDK.Skins.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    public class GetSkinsIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetSkinsIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void RequestUri_is_v2_skins()
        {
            var sut = new GetSkinsIndexRequest();

            var expected = new Uri("/v2/skins", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
