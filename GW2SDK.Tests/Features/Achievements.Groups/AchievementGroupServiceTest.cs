using System;
using System.Threading.Tasks;
using GW2SDK.Achievements.Groups;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    public class AchievementGroupServiceTest
    {
        private static class AchievementGroupFact
        {
            public static void Order_is_not_negative(AchievementGroup actual) => Assert.InRange(actual.Order, 0, int.MaxValue);
        }

        [Fact]
        public async Task It_can_get_all_achievement_groups()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroups();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.All(actual.Values,
                achievementGroup =>
                {
                    AchievementGroupFact.Order_is_not_negative(achievementGroup);
                });
        }

        [Fact]
        public async Task It_can_get_all_achievement_group_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroupsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_an_achievement_group_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            const string achievementCategoryId = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

            var actual = await sut.GetAchievementGroupById(achievementCategoryId);

            Assert.Equal(achievementCategoryId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_achievement_groups_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var ids = new[] { "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2", "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47", "B42E2379-9599-46CA-9D4A-40A27E192BBE" };

            var actual = await sut.GetAchievementGroupsByIds(ids);

            Assert.Collection(actual.Values,
                first => Assert.Equal(ids[0],  first.Id),
                second => Assert.Equal(ids[1], second.Id),
                third => Assert.Equal(ids[2],  third.Id));
        }

        [Fact]
        public async Task It_can_get_achievement_groups_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroupsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
