using GuildWars2.Emotes.Http;

namespace GuildWars2.Emotes;

[PublicAPI]
public sealed class EmotesQuery
{
    private readonly HttpClient http;

    public EmotesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/emotes

    public Task<(HashSet<string> Value, MessageContext Context)> GetUnlockedEmotes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedEmotesRequest { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/emotes

    public Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetEmotesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new EmotesIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Emote Value, MessageContext Context)> GetEmoteById(
        string emoteId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmoteByIdRequest request = new(emoteId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Emote> Value, MessageContext Context)> GetEmotesByIds(
        IReadOnlyCollection<string> emoteIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesByIdsRequest request =
            new(emoteIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
