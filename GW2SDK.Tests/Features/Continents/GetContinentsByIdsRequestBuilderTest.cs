using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Continents.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    public class GetContinentsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>("continentIds",
                () =>
                {
                    _ = new GetContinentsByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsEmpty_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>("continentIds",
                () =>
                {
                    _ = new GetContinentsByIdsRequest.Builder(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetContinentsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializeIdsAsQueryString()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetContinentsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/continents?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
