using System.Text.Json;

using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Gliders;

/// <summary>Provides query methods for glider skins and skins unlocked on the account.</summary>
[PublicAPI]
public sealed class GlidersClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="GlidersClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public GlidersClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/gliders

    /// <summary>Retrieves the IDs of glider skins unlocked on the account associated with the access token. This endpoint is
    /// only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedGliderSkins(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/gliders", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    #endregion v2/account/gliders

    #region v2/gliders

    /// <summary>Retrieves all glider skins.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GliderSkin> Value, MessageContext Context)> GetGliderSkins(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/gliders");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetGliderSkin());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all glider skins.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetGliderSkinsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/gliders");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a glider skin by its ID.</summary>
    /// <param name="gliderSkinId">The glider skin ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(GliderSkin Value, MessageContext Context)> GetGliderSkinById(
        int gliderSkinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/gliders");
        requestBuilder.Query.AddId(gliderSkinId);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetGliderSkin();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves glider skins by their IDs.</summary>
    /// <param name="gliderSkinIds">The glider skin IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GliderSkin> Value, MessageContext Context)> GetGliderSkinsByIds(
        IEnumerable<int> gliderSkinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/gliders");
        requestBuilder.Query.AddIds(gliderSkinIds);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetGliderSkin());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of glider skins.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GliderSkin> Value, MessageContext Context)> GetGliderSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/gliders");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetGliderSkin());
            return (value, response.Context);
        }
    }

    #endregion v2/gliders
}
