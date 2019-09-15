using System;
using System.Net.Http;
using GW2SDK.Achievements.Groups.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    public class GetAchievementGroupsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var ids = new[]
            {
                "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2",
                "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47",
                "B42E2379-9599-46CA-9D4A-40A27E192BBE"
            };

            var sut = new GetAchievementGroupsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new[]
            {
                "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2",
                "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47",
                "B42E2379-9599-46CA-9D4A-40A27E192BBE"
            };

            var sut = new GetAchievementGroupsByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri(
                "/v2/achievements/groups?ids=A4ED8379-5B6B-4ECC-B6E1-70C350C902D2,56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47,B42E2379-9599-46CA-9D4A-40A27E192BBE",
                UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void Achievement_group_ids_cannot_be_null()
        {
            Assert.Throws<ArgumentNullException>("achievementGroupIds",
                () =>
                {
                    _ = new GetAchievementGroupsByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void Achievement_group_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("achievementGroupIds",
                () =>
                {
                    _ = new GetAchievementGroupsByIdsRequest.Builder(new string[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void Achievement_group_ids_cannot_contain_null()
        {
            var ids = new[]
            {
                "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2",
                null,
                "B42E2379-9599-46CA-9D4A-40A27E192BBE"
            };

            Assert.Throws<ArgumentException>("achievementGroupIds",
                () =>
                {
                    _ = new GetAchievementGroupsByIdsRequest.Builder(ids);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public void Achievement_group_ids_cannot_contain_empty_id()
        {
            var ids = new[]
            {
                "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2",
                "",
                "B42E2379-9599-46CA-9D4A-40A27E192BBE"
            };

            Assert.Throws<ArgumentException>("achievementGroupIds",
                () =>
                {
                    _ = new GetAchievementGroupsByIdsRequest.Builder(ids);
                });
        }
    }
}
