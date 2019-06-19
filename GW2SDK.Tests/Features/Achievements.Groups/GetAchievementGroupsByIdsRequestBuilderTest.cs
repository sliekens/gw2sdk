using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Achievements.Groups;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    public class GetAchievementGroupsByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Achievement.Groups")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>("achievementGroupIds",
                () =>
                {
                    _ = new GetAchievementGroupsByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievement.Groups")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsEmpty_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>("achievementGroupIds",
                () =>
                {
                    _ = new GetAchievementGroupsByIdsRequest.Builder(new string[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievement.Groups")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsContainingNull_ShouldThrowArgumentException()
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
        [Trait("Feature",  "Achievement.Groups")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsContainingEmpty_ShouldThrowArgumentException()
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

        [Fact]
        [Trait("Feature",  "Achievement.Groups")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
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
        [Trait("Feature",  "Achievement.Groups")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializeIdsAsQueryString()
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
    }
}
