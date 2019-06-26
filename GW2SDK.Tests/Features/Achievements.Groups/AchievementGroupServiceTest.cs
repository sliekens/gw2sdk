using System;
using System.Threading.Tasks;
using GW2SDK.Features.Achievements.Groups;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    public class AchievementGroupServiceTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementGroups_ShouldReturnAllAchievementGroups()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroups();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementGroupsIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroupsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementGroupById_ShouldReturnThatAchievementGroup()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementGroupService>();

            var achievementCategoryId = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

            var actual = await sut.GetAchievementGroupById(achievementCategoryId);

            Assert.Equal(achievementCategoryId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Unit")]
        public async Task GetAchievementGroupsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
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
        public async Task GetAchievementGroupsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
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
        public async Task GetAchievementGroupsByIds_WithIdsContainingNull_ShouldThrowArgumentException()
        {
            var services = new Container();
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
        public async Task GetAchievementGroupsByIds_WithIdsContainingEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
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
        public async Task GetAchievementGroupsByIds_ShouldReturnThoseAchievementGroups()
        {
            var services = new Container();
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
        public async Task GetAchievementGroupsByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementGroupService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAchievementGroupsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementGroupsByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementGroupService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAchievementGroupsByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementGroupsByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementGroupService>();

            var actual = await sut.GetAchievementGroupsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
