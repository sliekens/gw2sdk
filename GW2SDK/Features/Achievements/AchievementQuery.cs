using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Http;
using GW2SDK.Achievements.Json;
using GW2SDK.Achievements.Models;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed class AchievementQuery
{
    private readonly HttpClient http;

    public AchievementQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    #region /v2/achievements/daily

    public async Task<IReplica<DailyAchievementGroup>> GetDailyAchievements(
        Day day = Day.Today,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DailyAchievementsRequest request = new(day);
        return await http.GetResource(request,
                json => DailyAchievementReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion

    #region /v2/achievements

    public async Task<IReplicaSet<int>> GetAchievementsIndex(CancellationToken cancellationToken = default)
    {
        AchievementsIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<Achievement>> GetAchievementById(
        int achievementId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementByIdRequest request = new(achievementId, language);
        return await http.GetResource(request,
                json => AchievementReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Achievement>> GetAchievementsByIds(
        IReadOnlyCollection<int> achievementIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementsByIdsRequest request = new(achievementIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => AchievementReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaPage<Achievement>> GetAchievementsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementsByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => AchievementReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion

    #region /v2/account/achievements

    [Scope(Permission.Progression)]
    public async Task<IReplica<AccountAchievement>> GetAccountAchievementById(
        int achievementId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountAchievementByIdRequest request = new(achievementId, accessToken);
        return await http.GetResource(request,
                json => AccountAchievementReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    [Scope(Permission.Progression)]
    public async Task<IReplicaSet<AccountAchievement>> GetAccountAchievementsByIds(
        IReadOnlyCollection<int> achievementIds,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountAchievementsByIdsRequest request = new(achievementIds, accessToken);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => AccountAchievementReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    [Scope(Permission.Progression)]
    public async Task<IReplicaSet<AccountAchievement>> GetAccountAchievements(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountAchievementsRequest request = new(accessToken);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => AccountAchievementReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    [Scope(Permission.Progression)]
    public async Task<IReplicaPage<AccountAchievement>> GetAccountAchievementsByPage(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountAchievementsByPageRequest request = new(pageIndex, pageSize, accessToken);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => AccountAchievementReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion

    #region /v2/achievements/categories

    public async Task<IReplicaSet<AchievementCategory>> GetAchievementCategories(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => AchievementCategoryReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetAchievementCategoriesIndex(CancellationToken cancellationToken = default)
    {
        AchievementCategoriesIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<AchievementCategory>> GetAchievementCategoryById(
        int achievementCategoryId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoryByIdRequest request = new(achievementCategoryId, language);
        return await http.GetResource(request,
                json => AchievementCategoryReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<AchievementCategory>> GetAchievementCategoriesByIds(
        IReadOnlyCollection<int> achievementCategoryIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesByIdsRequest request = new(achievementCategoryIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => AchievementCategoryReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaPage<AchievementCategory>> GetAchievementCategoriesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => AchievementCategoryReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion

    #region /v2/achievements/groups

    public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroups(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => AchievementGroupReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<string>> GetAchievementGroupsIndex(CancellationToken cancellationToken = default)
    {
        AchievementGroupsIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetStringArray(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<AchievementGroup>> GetAchievementGroupById(
        string achievementGroupId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupByIdRequest request = new(achievementGroupId, language);
        return await http.GetResource(request,
                json => AchievementGroupReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroupsByIds(
        IReadOnlyCollection<string> achievementGroupIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsByIdsRequest request = new(achievementGroupIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => AchievementGroupReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaPage<AchievementGroup>> GetAchievementGroupsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => AchievementGroupReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion

    #region /v2/titles

    public async Task<IReplicaSet<Title>> GetTitles(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TitlesRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => TitleReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetTitlesIndex(CancellationToken cancellationToken = default)
    {
        TitlesIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<Title>> GetTitleById(
        int titleId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TitleByIdRequest request = new(titleId, language);
        return await http.GetResource(request,
                json => TitleReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Title>> GetTitlesByIds(
        IReadOnlyCollection<int> titleIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TitlesByIdsRequest request = new(titleIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => TitleReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaPage<Title>> GetTitlesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TitlesByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => TitleReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion
}
