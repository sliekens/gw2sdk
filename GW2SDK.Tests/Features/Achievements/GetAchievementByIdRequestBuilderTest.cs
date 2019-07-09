using System;
using System.Net.Http;
using GW2SDK.Achievements.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class GetAchievementByIdRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var id = 1;

            var sut = new GetAchievementByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializeIdAsQueryString()
        {
            var id = 1;

            var sut = new GetAchievementByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/achievements?id=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
