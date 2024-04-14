using GuildWars2.Hero.Emotes.Http;
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
    public Task<(HashSet<string> Value, MessageContext Context)> GetUnlockedEmotes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedEmotesRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/emotes

    /// <summary>Retrieves all emotes.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        EmotesRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all emotes.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetEmotesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new EmotesIndexRequest();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves an emote by its ID.</summary>
    /// <param name="emoteId">The emote ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Emote Value, MessageContext Context)> GetEmoteById(
        string emoteId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        EmoteByIdRequest request = new(emoteId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves emotes by their IDs.</summary>
    /// <param name="emoteIds">The emote IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotesByIds(
        IEnumerable<string> emoteIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        EmotesByIdsRequest request = new(emoteIds.ToList());
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of emotes.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        EmotesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
