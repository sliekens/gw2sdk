using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Achievements;
using GuildWars2.Achievements.Categories;
using GuildWars2.Achievements.Dailies;
using GuildWars2.Achievements.Groups;
using GuildWars2.Achievements.Titles;
using GuildWars2.Http;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;
using static GuildWars2.ProductName;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementsQueryTest
{
    private static class AccountAchievementFact
    {
        public static void Id_is_positive(AccountAchievement actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);
    }

    private static class AchievementCategoryFact
    {
        public static void Id_is_positive(AchievementCategory actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Order_is_not_negative(AchievementCategory actual) =>
            Assert.InRange(actual.Order, 0, int.MaxValue);
    }

    private static class DailyAchievementFact
    {
        public static void Id_is_positive(DailyAchievement actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Min_level_is_between_1_and_80(DailyAchievement actual) =>
            Assert.InRange(actual.Level.Min, 1, 80);

        public static void Max_level_is_between_1_and_80(DailyAchievement actual) =>
            Assert.InRange(actual.Level.Max, 1, 80);

        public static void Can_have_a_product_requirement(DailyAchievement actual)
        {
            if (actual.RequiredAccess is not null)
            {
                Assert.Subset(
                    new HashSet<ProductName>
                    {
                        HeartOfThorns,
                        PathOfFire
                    },
                    new HashSet<ProductName> { actual.RequiredAccess.Product }
                );

                Assert.True(
                    Enum.IsDefined(typeof(AccessCondition), actual.RequiredAccess.Condition)
                );
            }
        }
    }

    private static class AchievementGroupFact
    {
        public static void Order_is_not_negative(AchievementGroup actual) =>
            Assert.InRange(actual.Order, 0, int.MaxValue);
    }

    private static class TitleFact
    {
        public static void Id_is_positive(Title actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Name_is_not_empty(Title actual) => Assert.NotEmpty(actual.Name);

        public static void Can_be_unlocked_by_achievements_or_achievement_points(Title actual)
        {
            if (actual.AchievementPointsRequired.HasValue)
            {
                Assert.InRange(actual.AchievementPointsRequired.Value, 1, 100000);
            }
            else
            {
                Assert.NotEmpty(actual.Achievements!);
            }
        }
    }

    [Theory]
    [InlineData(Day.Today)]
    [InlineData(Day.Tomorrow)]
    public async Task Daily_achievements_can_be_found_by_day(Day day)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var e = await Record.ExceptionAsync(async () =>
        {
            var actual = await sut.Achievements.GetDailyAchievements(day);

            Assert.All(actual.Value.Pve, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Pvp, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Wvw, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Fractals, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Special, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Pve, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Pvp, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Wvw, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Fractals, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Special, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Pve, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Pvp, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Wvw, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Fractals, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Special, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Pve, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Value.Pvp, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Value.Wvw, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Value.Fractals, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Value.Special, DailyAchievementFact.Can_have_a_product_requirement);
        });

        // Unavailable due to Wizard Vault changes
        var reason = Assert.IsType<GatewayException>(e);
        Assert.Equal("Service Unavailable", reason.Message);
    }

    [Fact]
    public async Task Achievements_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task An_achievement_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int achievementId = 1;

        var actual = await sut.Achievements.GetAchievementById(achievementId);

        Assert.Equal(achievementId, actual.Value.Id);
    }

    [Fact]
    public async Task Achievements_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> achievementIds = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Achievements.GetAchievementsByIds(achievementIds);

        Assert.Collection(
            actual.Value.OrderBy(achievement => achievement.Id),
            achievement => Assert.Equal(1, achievement.Id),
            achievement => Assert.Equal(2, achievement.Id),
            achievement => Assert.Equal(3, achievement.Id)
        );
    }

    [Fact]
    public async Task Achievements_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Account_achievements_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Achievements.GetAccountAchievements(accessToken.Key);

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);

        Assert.All(
            actual.Value,
            achievement =>
            {
                AccountAchievementFact.Id_is_positive(achievement);
            }
        );
    }

    [Fact]
    public async Task An_account_achievement_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int achievementId = 1;

        var actual =
            await sut.Achievements.GetAccountAchievementById(achievementId, accessToken.Key);

        Assert.Equal(achievementId, actual.Value.Id);
    }

    [Fact]
    public async Task Account_achievements_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Achievements.GetAccountAchievementsByIds(ids, accessToken.Key);

        Assert.Collection(
            actual.Value,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
        );
    }

    [Fact]
    public async Task Account_achievements_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Achievements.GetAccountAchievementsByPage(0, 3, accessToken.Key);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Achievement_categories_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementCategories();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            achievementCategory =>
            {
                AchievementCategoryFact.Id_is_positive(achievementCategory);
                AchievementCategoryFact.Order_is_not_negative(achievementCategory);
            }
        );
    }

    [Fact]
    public async Task Achievement_categories_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementCategoriesIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task An_achievement_category_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int achievementCategoryId = 1;

        var actual = await sut.Achievements.GetAchievementCategoryById(achievementCategoryId);

        Assert.Equal(achievementCategoryId, actual.Value.Id);
    }

    [Fact]
    public async Task Achievement_categories_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Achievements.GetAchievementCategoriesByIds(ids);

        Assert.Collection(
            actual.Value,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
        );
    }

    [Fact]
    public async Task Achievement_categories_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementCategoriesByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Achievement_groups_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementGroups();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            achievementGroup =>
            {
                AchievementGroupFact.Order_is_not_negative(achievementGroup);
            }
        );
    }

    [Fact]
    public async Task Achievement_groups_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementGroupsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task An_achievement_group_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string achievementCategoryId = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

        var actual = await sut.Achievements.GetAchievementGroupById(achievementCategoryId);

        Assert.Equal(achievementCategoryId, actual.Value.Id);
    }

    [Fact]
    public async Task Achievement_groups_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2",
            "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47",
            "B42E2379-9599-46CA-9D4A-40A27E192BBE"
        };

        var actual = await sut.Achievements.GetAchievementGroupsByIds(ids);

        Assert.Collection(
            actual.Value,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
        );
    }

    [Fact]
    public async Task Achievement_groups_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementGroupsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Titles_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetTitles();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            title =>
            {
                TitleFact.Id_is_positive(title);
                TitleFact.Name_is_not_empty(title);
                TitleFact.Can_be_unlocked_by_achievements_or_achievement_points(title);
            }
        );
    }

    [Fact]
    public async Task Titles_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetTitlesIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_title_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int titleId = 1;

        var actual = await sut.Achievements.GetTitleById(titleId);

        Assert.Equal(titleId, actual.Value.Id);
    }

    [Fact]
    public async Task Titles_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Achievements.GetTitlesByIds(ids);

        Assert.Collection(
            actual.Value,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
        );
    }

    [Fact]
    public async Task Titles_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetTitlesByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
