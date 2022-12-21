using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Emotes;

[PublicAPI]
public sealed class EmotesByIdsRequest : IHttpRequest<IReplicaSet<Emote>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/emotes") { AcceptEncoding = "gzip" };

    public EmotesByIdsRequest(IReadOnlyCollection<string> emoteIds)
    {
        Check.Collection(emoteIds, nameof(emoteIds));
        EmoteIds = emoteIds;
    }

    public IReadOnlyCollection<string> EmoteIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Emote>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", EmoteIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new ReplicaSet<Emote>
        {
            Values = json.RootElement.GetSet(entry => entry.GetEmote(MissingMemberBehavior)),
            Context = response.Headers.GetCollectionContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
