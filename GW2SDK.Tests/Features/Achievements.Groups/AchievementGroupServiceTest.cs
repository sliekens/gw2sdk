using System;
using System.Threading.Tasks;
using GW2SDK.Achievements.Groups;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    public class AchievementGroupServiceTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_achievement_groups()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroups();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_achievement_group_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroupsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_an_achievement_group_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var achievementCategoryId = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

            var actual = await sut.GetAchievementGroupById(achievementCategoryId);

            Assert.Equal(achievementCategoryId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public async Task Achievement_group_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            await Assert.ThrowsAsync<ArgumentNullException>("achievementGroupIds",
                async () =>
                {
                    await sut.GetAchievementGroupsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public async Task Achievement_group_ids_cannot_contain_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var ids = new[] { "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2", null, "B42E2379-9599-46CA-9D4A-40A27E192BBE" };

            await Assert.ThrowsAsync<ArgumentException>("achievementGroupIds",
                async () =>
                {
                    await sut.GetAchievementGroupsByIds(ids);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public async Task Achievement_group_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            await Assert.ThrowsAsync<ArgumentException>("achievementGroupIds",
                async () =>
                {
                    await sut.GetAchievementGroupsByIds(new string[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public async Task Achievement_group_ids_cannot_contain_empty_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var ids = new[] { "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2", "", "B42E2379-9599-46CA-9D4A-40A27E192BBE" };

            await Assert.ThrowsAsync<ArgumentException>("achievementGroupIds",
                async () =>
                {
                    await sut.GetAchievementGroupsByIds(ids);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_achievement_groups_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var ids = new[] { "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2", "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47", "B42E2379-9599-46CA-9D4A-40A27E192BBE" };

            var actual = await sut.GetAchievementGroupsByIds(ids);

            Assert.Collection(actual,
                first => Assert.Equal(ids[0],  first.Id),
                second => Assert.Equal(ids[1], second.Id),
                third => Assert.Equal(ids[2],  third.Id));
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_achievement_groups_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroupsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAchievementGroupsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAchievementGroupsByPage(1, -3));
        }
    }
}
