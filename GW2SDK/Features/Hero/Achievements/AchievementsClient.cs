using System.Runtime.CompilerServices;
using GuildWars2.Hero.Achievements.Categories;
using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Hero.Achievements.Http;
using GuildWars2.Hero.Achievements.Titles;

namespace GuildWars2.Hero.Achievements;

/// <summary>Provides query methods for achievements and titles in the game and achievement progress on the account.</summary>
[PublicAPI]
public sealed class AchievementsClient
{
    private readonly HttpClient httpClient;

    public AchievementsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/titles

    /// <summary>Retrieves the IDs of unlocked titles on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedTitles(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedTitlesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/titles

    #region v2/achievements

    /// <summary>Retrieves the IDs of all achievements.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetAchievementsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves an achievement by its ID.</summary>
    /// <param name="achievementId">The achievement ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Achievement Value, MessageContext Context)> GetAchievementById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves achievements by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="achievementIds">The achievement IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Achievement> Value, MessageContext Context)> GetAchievementsByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of achievements.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Achievement> Value, MessageContext Context)> GetAchievementsByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves achievements by their IDs by chunking requests and executing them in parallel. Supports more than
    /// 200 IDs.</summary>
    /// <param name="achievementIds">The achievement IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public IAsyncEnumerable<(Achievement Value, MessageContext Context)> GetAchievementsBulk(
        IReadOnlyCollection<int> achievementIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            achievementIds,
            GetChunk,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );

        // ReSharper disable once VariableHidesOuterVariable (intended, believe it or not)
        async Task<IReadOnlyCollection<(Achievement, MessageContext)>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var (values, context) = await GetAchievementsByIds(
                    chunk,
                    language,
                    missingMemberBehavior,
                    cancellationToken
                )
                .ConfigureAwait(false);
            return values.Select(value => (item: value, context)).ToList();
        }
    }

    /// <summary>Retrieves all achievements by chunking requests and executing them in parallel.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public async IAsyncEnumerable<(Achievement Value, MessageContext Context)> GetAchievementsBulk(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var (value, _) = await GetAchievementsIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetAchievementsBulk(
            value,
            language,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var achievement in producer.ConfigureAwait(false))
        {
            yield return achievement;
        }
    }

    #endregion v2/achievements

    #region v2/account/achievements

    /// <summary>Retrieves achievement progress on the account by its ID. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="achievementId">The achievement ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(AccountAchievement Value, MessageContext Context)> GetAccountAchievementById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves achievement progress on the account by their IDs. This endpoint is only accessible with a valid
    /// access token</summary>
    /// <param name="achievementIds">The achievement IDs.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AccountAchievement> Value, MessageContext Context)>
        GetAccountAchievementsByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves all achievement progress on the account. This endpoint is only accessible with a valid access token</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AccountAchievement> Value, MessageContext Context)> GetAccountAchievements(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of achievement progress on the account. This endpoint is only accessible with a valid access
    /// token</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AccountAchievement> Value, MessageContext Context)>
        GetAccountAchievementsByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/achievements

    #region v2/achievements/categories

    /// <summary>Retrieves all achievement categories.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AchievementCategory> Value, MessageContext Context)>
        GetAchievementCategories(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all achievement categories.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetAchievementCategoriesIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementCategoriesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves an achievement category by its ID.</summary>
    /// <param name="achievementCategoryId">the achievement category ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(AchievementCategory Value, MessageContext Context)> GetAchievementCategoryById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves achievement categories by their IDs.</summary>
    /// <param name="achievementCategoryIds">The achievement category IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AchievementCategory> Value, MessageContext Context)>
        GetAchievementCategoriesByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of achievement categories.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AchievementCategory> Value, MessageContext Context)>
        GetAchievementCategoriesByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/achievements/categories

    #region v2/achievements/groups

    /// <summary>Retrieves all achievement groups.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AchievementGroup> Value, MessageContext Context)> GetAchievementGroups(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all achievement groups.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetAchievementGroupsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AchievementGroupsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves an achievement group by its ID.</summary>
    /// <param name="achievementGroupId">The achievement group ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(AchievementGroup Value, MessageContext Context)> GetAchievementGroupById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves achievement groups by their IDs.</summary>
    /// <param name="achievementGroupIds">The achievement group IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AchievementGroup> Value, MessageContext Context)>
        GetAchievementGroupsByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of achievement groups.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AchievementGroup> Value, MessageContext Context)>
        GetAchievementGroupsByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/achievements/groups

    #region v2/titles

    /// <summary>Retrieves all titles.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Title> Value, MessageContext Context)> GetTitles(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all titles.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetTitlesIndex(
        CancellationToken cancellationToken = default
    )
    {
        TitlesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a title by its ID.</summary>
    /// <param name="titleId">The title ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Title Value, MessageContext Context)> GetTitleById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves titles by their IDs.</summary>
    /// <param name="titleIds">The title IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Title> Value, MessageContext Context)> GetTitlesByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of titles.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Title> Value, MessageContext Context)> GetTitlesByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/titles
}
