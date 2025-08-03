using System.Text.Json;

using GuildWars2.Http;
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
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/mounts

    /// <summary>Retrieves the IDs of mounts unlocked on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Extensible<MountName>> Value, MessageContext Context)>
        GetUnlockedMounts(string? accessToken, CancellationToken cancellationToken = default)
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/mounts/types", accessToken);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMountName());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of mount skins unlocked on the account associated with the access token. This endpoint is
    /// only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMountSkins(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/mounts/skins", accessToken);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    #endregion v2/account/mounts

    #region v2/mounts/types

    /// <summary>Retrieves all mounts.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Mount> Value, MessageContext Context)> GetMounts(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/types");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMount());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the names of all mounts.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Extensible<MountName>> Value, MessageContext Context)> GetMountNames(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/types");
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMountName());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a mount by its name.</summary>
    /// <param name="mountName">The mount name.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Mount Value, MessageContext Context)> GetMountByName(
        Extensible<MountName> mountName,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/types");
        requestBuilder.Query.AddId(MountNameFormatter.FormatMountName(mountName.ToString()));
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMount();
            return (value, response.Context);
        }
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
        in CancellationToken cancellationToken = default
    )
    {
        return GetMountsByNames(
            mountNames.Select(mountName => (Extensible<MountName>)mountName),
            language,
            missingMemberBehavior,
            cancellationToken
        );
    }

    /// <summary>Retrieves mounts by their names.</summary>
    /// <param name="mountNames">The mount names.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Mount> Value, MessageContext Context)> GetMountsByNames(
        IEnumerable<Extensible<MountName>> mountNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/types");
        requestBuilder.Query.AddIds(mountNames.Select(MountNameFormatter.FormatMountName));
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMount());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of mounts.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Mount> Value, MessageContext Context)> GetMountsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/types");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMount());
            return (value, response.Context);
        }
    }

    #endregion v2/mounts/types

    #region v2/mounts/skins

    /// <summary>Retrieves all mount skins.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkins(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/skins");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMountSkin());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all mount skins.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetMountSkinsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/skins");
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a mount skin by its ID.</summary>
    /// <param name="mountSkinId">The mount skin ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MountSkin Value, MessageContext Context)> GetMountSkinById(
        int mountSkinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/skins");
        requestBuilder.Query.AddId(mountSkinId);
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMountSkin();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves mount skins by their IDs.</summary>
    /// <param name="mountSkinIds">The mount skin IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkinsByIds(
        IEnumerable<int> mountSkinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/skins");
        requestBuilder.Query.AddIds(mountSkinIds);
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMountSkin());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of mount skins.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MountSkin> Value, MessageContext Context)> GetMountSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mounts/skins");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMountSkin());
            return (value, response.Context);
        }
    }

    #endregion v2/mounts/skins
}
