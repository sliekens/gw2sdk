using System;
using System.Net.Http;
using GW2SDK.Achievements.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class GetAchievementsIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetAchievementsIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_is_v2_achievements()
        {
            var sut = new GetAchievementsIndexRequest();

            var expected = new Uri("/v2/achievements", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
