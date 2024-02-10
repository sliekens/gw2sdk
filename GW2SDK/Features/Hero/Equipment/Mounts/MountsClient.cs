using GuildWars2.Hero.Equipment.Mounts.Http;

namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>Provides query methods for mounts and mounts unlocked on the account.</summary>
[PublicAPI]
public sealed class MountsClient
{
    private readonly HttpClient httpClient;

    public MountsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/mounts

    public Task<(HashSet<MountName> Value, MessageContext Context)> GetUnlockedMounts(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedMountsRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMountSkins(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedMountSkinsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/mounts

    public Task<(HashSet<Mount> Value, MessageContext Context)> GetMounts(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MountName> Value, MessageContext Context)> GetMountNames(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountNamesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Mount Value, MessageContext Context)> GetMountByName(
        MountName mountName,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountByNameRequest request = new(mountName)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Mount> Value, MessageContext Context)> GetMountsByNames(
        IReadOnlyCollection<MountName> mountNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountsByNamesRequest request = new(mountNames)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Mount> Value, MessageContext Context)> GetMountsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkins(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountSkinsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetMountSkinsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MountSkinsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MountSkin Value, MessageContext Context)> GetMountSkinById(
        int mountSkinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountSkinByIdRequest request = new(mountSkinId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkinsByIds(
        IReadOnlyCollection<int> mountSkinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountSkinsByIdsRequest request = new(mountSkinIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MountSkinsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
