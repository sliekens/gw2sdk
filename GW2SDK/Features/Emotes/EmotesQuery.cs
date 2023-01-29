using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Annotations;
using JetBrains.Annotations;

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

    #region v2/emotes

    public Task<Replica<HashSet<Emote>>> GetEmotes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetEmotesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new EmotesIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Emote>> GetEmoteById(
        string emoteId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmoteByIdRequest request = new(emoteId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Emote>>> GetEmotesByIds(
        IReadOnlyCollection<string> emoteIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesByIdsRequest request =
            new(emoteIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Emote>>> GetEmotesByPage(
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

    #region v2/account/emotes

    [Scope(Permission.Progression, Permission.Unlocks)]

    public Task<Replica<HashSet<string>>> GetUnlockedEmotes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedEmotesRequest
        {
            AccessToken = accessToken
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
