using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Emotes;

[PublicAPI]
public sealed class EmotesByIdsRequest : IHttpRequest<IReplicaSet<Emote>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/emotes") { AcceptEncoding = "gzip" };

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
                Template with { Arguments = new QueryBuilder { { "ids", EmoteIds } } },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetEmote(MissingMemberBehavior));
        return new ReplicaSet<Emote>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
