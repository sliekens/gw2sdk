using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Achievements;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class GetAchievementIdsRequestTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetAchievementIdsRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Achievements()
        {
            var sut = new GetAchievementIdsRequest();

            var expected = new Uri("/v2/achievements", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
