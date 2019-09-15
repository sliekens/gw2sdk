using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Accounts.Achievements.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class GetAccountAchievementsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void Achievement_ids_cannot_be_null()
        {
            Assert.Throws<ArgumentNullException>("achievementIds",
                () =>
                {
                    _ = new GetAccountAchievementsByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void Achievement_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("achievementIds",
                () =>
                {
                    _ = new GetAccountAchievementsByIdsRequest.Builder(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAccountAchievementsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAccountAchievementsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/account/achievements?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
