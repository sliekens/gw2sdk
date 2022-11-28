using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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

    public Task<IReplicaSet<Emote>> GetEmotes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<string>> GetEmotesIndex(CancellationToken cancellationToken = default)
    {
        var request = new EmotesIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Emote>> GetEmoteById(
        string emoteId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmoteByIdRequest request = new(emoteId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Emote>> GetEmotesByIds(
        IReadOnlyCollection<string> emoteIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmotesByIdsRequest request =
            new(emoteIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Emote>> GetEmotesByPage(
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
