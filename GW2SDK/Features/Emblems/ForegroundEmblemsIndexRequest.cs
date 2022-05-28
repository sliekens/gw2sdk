using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;

namespace GW2SDK.Emblems;

public sealed class ForegroundEmblemsIndexRequest : IHttpRequest<IReplicaSet<int>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/emblem/foregrounds") { AcceptEncoding = "gzip" };

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
