using GuildWars2.BuildStorage.Http;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public sealed record BuildStorageQuery
{
    private readonly HttpClient http;

    public BuildStorageQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/buildstorage

    public Task<Replica<HashSet<Build>>> GetBuildStorage(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildStorageRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetBuildStorageIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        BuildStorageIndexRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Build>> GetBuildStorageSpaceById(
        string? accessToken,
        int buildStorageId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildByIdRequest request = new(buildStorageId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Build>>> GetBuildStorageSpacesByIds(
        string? accessToken,
        IReadOnlyCollection<int> buildStorageIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildsByIdsRequest request = new(buildStorageIds)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Build>>> GetBuildStorageSpacesByPage(
        string? accessToken,
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/buildstorage

    #region v2/characters/:id/buildtabs

    public Task<Replica<HashSet<int>>> GetBuildTabsIndex(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        BuildTabsIndexRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<BuildTab>> GetBuildTab(
        string characterName,
        int tab,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        BuildTabRequest request = new(characterName, tab) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<BuildTab>>> GetBuildTabs(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        BuildTabsRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<BuildTab>> GetActiveBuildTab(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        ActiveBuildTabRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/characters/:id/buildtabs
}
