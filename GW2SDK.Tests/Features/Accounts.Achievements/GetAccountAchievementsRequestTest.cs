using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Accounts.Achievements;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class GetAccountAchievementsRequestTest
    {
        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetAccountAchievementsRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2AccountAchievementsBulk()
        {
            var sut = new GetAccountAchievementsRequest();

            var expected = new Uri("/v2/account/achievements?ids=all", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
