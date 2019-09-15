using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Worlds.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class GetWorldsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetWorldsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetWorldsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/worlds?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void World_ids_cannot_be_null()
        {
            Assert.Throws<ArgumentNullException>("worldIds",
                () =>
                {
                    _ = new GetWorldsByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void World_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("worldIds",
                () =>
                {
                    _ = new GetWorldsByIdsRequest.Builder(new int[0]);
                });
        }
    }
}
