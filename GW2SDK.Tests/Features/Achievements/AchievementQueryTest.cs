﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Achievements;
using GW2SDK.Achievements.Models;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;
using static GW2SDK.ProductName;

namespace GW2SDK.Tests.Features.Achievements;

public class AchievementQueryTest
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

    [Theory(Skip = "Daily achievements are not working right...")]
    [InlineData(Day.Today)]
    [InlineData(Day.Tomorrow)]
    public async Task It_can_get_get_daily_achievements(Day day)
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetDailyAchievements(day);

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

    [Fact]
    public async Task It_can_get_all_achievement_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetAchievementsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_an_achievement_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        const int achievementId = 1;

        var actual = await sut.GetAchievementById(achievementId);

        Assert.Equal(achievementId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_achievements_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        HashSet<int> achievementIds = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetAchievementsByIds(achievementIds);

        Assert.Collection(
            actual,
            achievement => Assert.Equal(1, achievement.Id),
            achievement => Assert.Equal(2, achievement.Id),
            achievement => Assert.Equal(3, achievement.Id)
            );
    }

    [Fact]
    public async Task It_can_get_achievements_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetAchievementsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task It_can_get_all_account_achievements()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();
        var accessToken = services.Resolve<ApiKeyFull>();

        var actual = await sut.GetAccountAchievements(accessToken.Key);

        Assert.Equal(actual.Context.ResultTotal, actual.Count);

        Assert.All(
            actual,
            achievement =>
            {
                AccountAchievementFact.Id_is_positive(achievement);
            }
            );
    }

    [Fact]
    public async Task It_can_get_an_account_achievement_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();
        var accessToken = services.Resolve<ApiKeyFull>();

        const int achievementId = 1;

        var actual = await sut.GetAccountAchievementById(achievementId, accessToken.Key);

        Assert.Equal(achievementId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_account_achievements_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();
        var accessToken = services.Resolve<ApiKeyFull>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetAccountAchievementsByIds(ids, accessToken.Key);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
            );
    }

    [Fact]
    public async Task It_can_get_account_achievements_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();
        var accessToken = services.Resolve<ApiKeyFull>();

        var actual = await sut.GetAccountAchievementsByPage(0, 3, accessToken.Key);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task It_can_get_all_achievement_categories()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetAchievementCategories();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            achievementCategory =>
            {
                AchievementCategoryFact.Id_is_positive(achievementCategory);
                AchievementCategoryFact.Order_is_not_negative(achievementCategory);
            }
            );
    }

    [Fact]
    public async Task It_can_get_all_achievement_category_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetAchievementCategoriesIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_an_achievement_category_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        const int achievementCategoryId = 1;

        var actual = await sut.GetAchievementCategoryById(achievementCategoryId);

        Assert.Equal(achievementCategoryId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_achievement_categories_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetAchievementCategoriesByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
            );
    }

    [Fact]
    public async Task It_can_get_achievement_categories_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetAchievementCategoriesByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task It_can_get_all_achievement_groups()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetAchievementGroups();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            achievementGroup =>
            {
                AchievementGroupFact.Order_is_not_negative(achievementGroup);
            }
            );
    }

    [Fact]
    public async Task It_can_get_all_achievement_group_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetAchievementGroupsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_an_achievement_group_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        const string achievementCategoryId = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

        var actual = await sut.GetAchievementGroupById(achievementCategoryId);

        Assert.Equal(achievementCategoryId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_achievement_groups_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        HashSet<string> ids = new()
        {
            "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2",
            "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47",
            "B42E2379-9599-46CA-9D4A-40A27E192BBE"
        };

        var actual = await sut.GetAchievementGroupsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
            );
    }

    [Fact]
    public async Task It_can_get_achievement_groups_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetAchievementGroupsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task It_can_get_all_titles()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetTitles();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            title =>
            {
                TitleFact.Id_is_positive(title);
                TitleFact.Name_is_not_empty(title);
                TitleFact.Can_be_unlocked_by_achievements_or_achievement_points(title);
            }
            );
    }

    [Fact]
    public async Task It_can_get_all_title_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetTitlesIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_title_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        const int titleId = 1;

        var actual = await sut.GetTitleById(titleId);

        Assert.Equal(titleId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_titles_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetTitlesByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
            );
    }

    [Fact]
    public async Task It_can_get_titles_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<AchievementQuery>();

        var actual = await sut.GetTitlesByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}
