using GuildWars2.Hero.Emotes.Http;

namespace GuildWars2.Hero.Emotes;

[PublicAPI]
public sealed class EmotesClient
{
    private readonly HttpClient httpClient;

    public EmotesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/emotes

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

    public Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetEmotesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new EmotesIndexRequest();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Emote Value, MessageContext Context)> GetEmoteById(
        string emoteId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmoteByIdRequest request = new(emoteId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotesByIds(
        IReadOnlyCollection<string> emoteIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesByIdsRequest request =
            new(emoteIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
