using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Skins.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    public class GetSkinsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>("skinIds",
                () =>
                {
                    _ = new GetSkinsByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsEmpty_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>("skinIds",
                () =>
                {
                    _ = new GetSkinsByIdsRequest.Builder(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetSkinsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializeIdsAsQueryString()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetSkinsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/skins?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
