using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Achievements.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class GetAchievementsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAchievementsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void Achievement_ids_cannot_be_null()
        {
            Assert.Throws<ArgumentNullException>("achievementIds",
                () =>
                {
                    _ = new GetAchievementsByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void Achievement_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("achievementIds",
                () =>
                {
                    _ = new GetAchievementsByIdsRequest.Builder(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAchievementsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/achievements?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
