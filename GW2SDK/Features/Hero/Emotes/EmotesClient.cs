using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Emotes;

/// <summary>Provides query methods for emotes and unlocked emotes on the account.</summary>
[PublicAPI]
public sealed class EmotesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="EmotesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public EmotesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/emotes

    /// <summary>Retrieves the IDs of emotes unlocked on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetUnlockedEmotes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/emotes", accessToken);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    #endregion

    #region v2/emotes

    /// <summary>Retrieves all emotes.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/emotes");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetEmote());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all emotes.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetEmotesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/emotes");
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves an emote by its ID.</summary>
    /// <param name="emoteId">The emote ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Emote Value, MessageContext Context)> GetEmoteById(
        string emoteId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/emotes");
        requestBuilder.Query.AddId(emoteId);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetEmote();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves emotes by their IDs.</summary>
    /// <param name="emoteIds">The emote IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotesByIds(
        IEnumerable<string> emoteIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/emotes");
        requestBuilder.Query.AddIds(emoteIds);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetEmote());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of emotes.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/emotes");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetEmote());
            return (value, response.Context);
        }
    }

    #endregion
}
