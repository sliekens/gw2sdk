﻿using GuildWars2.Hero.Equipment.Mounts.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>Provides query methods for mounts and mounts unlocked on the account.</summary>
[PublicAPI]
public sealed class MountsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="MountsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public MountsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/mounts

    /// <summary>Retrieves the IDs of mounts unlocked on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Extensible<MountName>> Value, MessageContext Context)> GetUnlockedMounts(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedMountsRequest request = new()
        {
            AccessToken = accessToken
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of mount skins unlocked on the account associated with the access token. This endpoint is
    /// only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMountSkins(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedMountSkinsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/mounts/types

    /// <summary>Retrieves all mounts.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Mount> Value, MessageContext Context)> GetMounts(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MountsRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the names of all mounts.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Extensible<MountName>> Value, MessageContext Context)> GetMountNames(
        CancellationToken cancellationToken = default
    )
    {
        MountNamesRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a mount by its name.</summary>
    /// <param name="mountName">The mount name.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Mount Value, MessageContext Context)> GetMountByName(
        MountName mountName,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MountByNameRequest request = new(mountName)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves mounts by their names.</summary>
    /// <param name="mountNames">The mount names.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Mount> Value, MessageContext Context)> GetMountsByNames(
        IEnumerable<MountName> mountNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MountsByNamesRequest request = new(mountNames.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of mounts.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Mount> Value, MessageContext Context)> GetMountsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MountsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/mounts/types

    #region v2/mounts/skins

    /// <summary>Retrieves all mount skins.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkins(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MountSkinsRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the ids of all mount skins.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetMountSkinsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MountSkinsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a mount skin by its ID.</summary>
    /// <param name="mountSkinId">The mount skin ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MountSkin Value, MessageContext Context)> GetMountSkinById(
        int mountSkinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MountSkinByIdRequest request = new(mountSkinId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves mount skins by their IDs.</summary>
    /// <param name="mountSkinIds">The mount skin IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkinsByIds(
        IEnumerable<int> mountSkinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MountSkinsByIdsRequest request = new(mountSkinIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of mount skins.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MountSkinsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/mounts/skins
}
