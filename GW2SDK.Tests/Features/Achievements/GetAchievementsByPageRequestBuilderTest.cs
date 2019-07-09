using System;
using System.Net.Http;
using GW2SDK.Achievements.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class GetAchievementsByPageRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var sut = new GetAchievementsByPageRequest.Builder(0);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializePageAsQueryString()
        {
            var sut = new GetAchievementsByPageRequest.Builder(1);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/achievements?page=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void GetRequest_WithPageSize_ShouldSerializePageSizeAsQueryString()
        {
            var sut = new GetAchievementsByPageRequest.Builder(1, 200);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/achievements?page=1&page_size=200", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
