using System;
using System.Net.Http;
using GW2SDK.Accounts.Achievements.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class GetAccountAchievementsByPageRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetAccountAchievementsByPageRequest.Builder(0);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_page()
        {
            var sut = new GetAccountAchievementsByPageRequest.Builder(1);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/account/achievements?page=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_page_size()
        {
            var sut = new GetAccountAchievementsByPageRequest.Builder(1, 200);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/account/achievements?page=1&page_size=200", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
