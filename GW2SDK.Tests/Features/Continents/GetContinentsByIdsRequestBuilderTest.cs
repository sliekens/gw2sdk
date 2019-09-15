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
        public void Method_is_GET()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetContinentsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetContinentsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/continents?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void Continent_ids_cannot_be_null()
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
        public void Continent_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("continentIds",
                () =>
                {
                    _ = new GetContinentsByIdsRequest.Builder(new int[0]);
                });
        }
    }
}
