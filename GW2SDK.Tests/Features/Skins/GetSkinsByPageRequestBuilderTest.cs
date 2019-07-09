using System;
using System.Net.Http;
using GW2SDK.Skins.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    public class GetSkinsByPageRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var sut = new GetSkinsByPageRequest.Builder(0);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializePageAsQueryString()
        {
            var sut = new GetSkinsByPageRequest.Builder(1);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/skins?page=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void GetRequest_WithPageSize_ShouldSerializePageSizeAsQueryString()
        {
            var sut = new GetSkinsByPageRequest.Builder(1, 200);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/skins?page=1&page_size=200", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
