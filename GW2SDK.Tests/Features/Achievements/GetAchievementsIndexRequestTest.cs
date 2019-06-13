using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Achievements;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class GetAchievementsIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetAchievementsIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Achievements()
        {
            var sut = new GetAchievementsIndexRequest();

            var expected = new Uri("/v2/achievements", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
