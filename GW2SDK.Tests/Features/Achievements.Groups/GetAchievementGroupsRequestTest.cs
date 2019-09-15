﻿using System;
using System.Net.Http;
using GW2SDK.Achievements.Groups.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    public class GetAchievementGroupsRequestTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetAchievementGroupsRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void RequestUri_is_v2_achievements_groups_bulk()
        {
            var sut = new GetAchievementGroupsRequest();

            var expected = new Uri("/v2/achievements/groups?ids=all", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
