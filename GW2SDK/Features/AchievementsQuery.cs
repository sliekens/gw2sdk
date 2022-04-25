using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements;
using GW2SDK.Achievements;
using GW2SDK.Achievements.Categories;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Achievements.Groups;
using GW2SDK.Achievements.Titles;
using GW2SDK.Annotations;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class AchievementsQuery
{
    private readonly HttpClient http;

    public AchievementsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region /v2/achievements/daily

    public Task<IReplica<DailyAchievementGroup>> GetDailyAchievements(
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

    #region /v2/achievements

    public Task<IReplicaSet<int>> GetAchievementsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Achievement>> GetAchievementById(
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

    public Task<IReplicaSet<Achievement>> GetAchievementsByIds(
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

    public Task<IReplicaPage<Achievement>> GetAchievementsByPage(
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

    #region /v2/account/achievements

    [Scope(Permission.Progression)]
    public Task<IReplica<AccountAchievement>> GetAccountAchievementById(
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
    public Task<IReplicaSet<AccountAchievement>> GetAccountAchievementsByIds(
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
    public Task<IReplicaSet<AccountAchievement>> GetAccountAchievements(
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
    public Task<IReplicaPage<AccountAchievement>> GetAccountAchievementsByPage(
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

    #region /v2/achievements/categories

    public Task<IReplicaSet<AchievementCategory>> GetAchievementCategories(
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

    public Task<IReplicaSet<int>> GetAchievementCategoriesIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<AchievementCategory>> GetAchievementCategoryById(
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

    public Task<IReplicaSet<AchievementCategory>> GetAchievementCategoriesByIds(
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

    public Task<IReplicaPage<AchievementCategory>> GetAchievementCategoriesByPage(
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

    #region /v2/achievements/groups

    public Task<IReplicaSet<AchievementGroup>> GetAchievementGroups(
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

    public Task<IReplicaSet<string>> GetAchievementGroupsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<AchievementGroup>> GetAchievementGroupById(
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

    public Task<IReplicaSet<AchievementGroup>> GetAchievementGroupsByIds(
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

    public Task<IReplicaPage<AchievementGroup>> GetAchievementGroupsByPage(
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

    #region /v2/titles

    public Task<IReplicaSet<Title>> GetTitles(
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

    public Task<IReplicaSet<int>> GetTitlesIndex(CancellationToken cancellationToken = default)
    {
        TitlesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Title>> GetTitleById(
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

    public Task<IReplicaSet<Title>> GetTitlesByIds(
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

    public Task<IReplicaPage<Title>> GetTitlesByPage(
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
