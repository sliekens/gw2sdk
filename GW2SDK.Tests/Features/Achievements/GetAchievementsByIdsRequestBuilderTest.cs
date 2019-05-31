using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GW2SDK.Infrastructure.Achievements;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class GetAchievementsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsNull_ShouldThrowArgumentNullException()
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
        public void Constructor_WithIdsEmpty_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>("achievementIds",
                () =>
                {
                    _ = new GetAchievementsByIdsRequest.Builder(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAchievementsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializeIdsAsQueryString()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAchievementsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/achievements?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
