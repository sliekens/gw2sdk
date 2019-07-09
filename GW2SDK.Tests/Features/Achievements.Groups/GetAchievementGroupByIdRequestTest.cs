using System;
using System.Net.Http;
using GW2SDK.Achievements.Groups.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    public class GetAchievementGroupByIdRequestTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var id = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

            var sut = new GetAchievementGroupByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializeIdAsQueryString()
        {
            var id = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

            var sut = new GetAchievementGroupByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/achievements/groups?id=A4ED8379-5B6B-4ECC-B6E1-70C350C902D2", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
