using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementsQueryTest
{
    [Theory]
    [InlineData(Day.Today)]
    [InlineData(Day.Tomorrow)]
    public async Task Daily_achievements_can_be_found_by_day(Day day)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var e = await Record.ExceptionAsync(async () =>
        {
            var actual = await sut.Achievements.GetDailyAchievements(day);
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

        const int id = 1;

        var actual = await sut.Achievements.GetAchievementById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Achievements_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Achievements.GetAchievementsByIds(ids);

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
                achievement.Id_is_positive();
            }
        );
    }

    [Fact]
    public async Task An_account_achievement_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int id = 1;

        var actual =
            await sut.Achievements.GetAccountAchievementById(id, accessToken.Key);

        Assert.Equal(id, actual.Value.Id);
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
                achievementCategory.Id_is_positive();
                achievementCategory.Order_is_not_negative();
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

        const int id = 1;

        var actual = await sut.Achievements.GetAchievementCategoryById(id);

        Assert.Equal(id, actual.Value.Id);
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
                achievementGroup.Order_is_not_negative();
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

        const string id = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

        var actual = await sut.Achievements.GetAchievementGroupById(id);

        Assert.Equal(id, actual.Value.Id);
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
                title.Id_is_positive();
                title.Name_is_not_empty();
                title.Can_be_unlocked_by_achievements_or_achievement_points();
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

        const int id = 1;

        var actual = await sut.Achievements.GetTitleById(id);

        Assert.Equal(id, actual.Value.Id);
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
