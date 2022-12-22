using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Achievements.Categories;
using GuildWars2.Achievements.Dailies;
using GuildWars2.Achievements.Groups;
using GuildWars2.Achievements.Titles;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
public sealed class AchievementsQuery
{
    private readonly HttpClient http;

    public AchievementsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/achievements/daily

    public Task<Replica<DailyAchievementGroup>> GetDailyAchievements(
        Day day = Day.Today,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DailyAchievementsRequest request = new()
        {
            Day = day,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/achievements

    public Task<Replica<HashSet<int>>> GetAchievementsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Achievement>> GetAchievementById(
        int achievementId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementByIdRequest request = new(achievementId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Achievement>>> GetAchievementsByIds(
        IReadOnlyCollection<int> achievementIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementsByIdsRequest request = new(achievementIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Achievement>>> GetAchievementsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/account/achievements

    [Scope(Permission.Progression)]
    public Task<Replica<AccountAchievement>> GetAccountAchievementById(
        int achievementId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountAchievementByIdRequest request = new(achievementId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    [Scope(Permission.Progression)]
    public Task<Replica<HashSet<AccountAchievement>>> GetAccountAchievementsByIds(
        IReadOnlyCollection<int> achievementIds,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountAchievementsByIdsRequest request = new(achievementIds)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    [Scope(Permission.Progression)]
    public Task<Replica<HashSet<AccountAchievement>>> GetAccountAchievements(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountAchievementsRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    [Scope(Permission.Progression)]
    public Task<Replica<HashSet<AccountAchievement>>> GetAccountAchievementsByPage(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountAchievementsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/achievements/categories

    public Task<Replica<HashSet<AchievementCategory>>> GetAchievementCategories(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetAchievementCategoriesIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<AchievementCategory>> GetAchievementCategoryById(
        int achievementCategoryId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoryByIdRequest request = new(achievementCategoryId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<AchievementCategory>>> GetAchievementCategoriesByIds(
        IReadOnlyCollection<int> achievementCategoryIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesByIdsRequest request = new(achievementCategoryIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<AchievementCategory>>> GetAchievementCategoriesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/achievements/groups

    public Task<Replica<HashSet<AchievementGroup>>> GetAchievementGroups(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetAchievementGroupsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<AchievementGroup>> GetAchievementGroupById(
        string achievementGroupId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupByIdRequest request = new(achievementGroupId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<AchievementGroup>>> GetAchievementGroupsByIds(
        IReadOnlyCollection<string> achievementGroupIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsByIdsRequest request = new(achievementGroupIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<AchievementGroup>>> GetAchievementGroupsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/titles

    public Task<Replica<HashSet<Title>>> GetTitles(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TitlesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetTitlesIndex(
        CancellationToken cancellationToken = default
    )
    {
        TitlesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Title>> GetTitleById(
        int titleId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TitleByIdRequest request = new(titleId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Title>>> GetTitlesByIds(
        IReadOnlyCollection<int> titleIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TitlesByIdsRequest request = new(titleIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Title>>> GetTitlesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TitlesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
