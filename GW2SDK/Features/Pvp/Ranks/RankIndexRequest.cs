using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
public sealed class RankIndexRequest : IHttpRequest<IReplicaSet<int>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/pvp/ranks")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public async Task<IReplicaSet<int>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response =
            await httpClient.SendAsync(Template, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetInt32());
        return new ReplicaSet<int>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
