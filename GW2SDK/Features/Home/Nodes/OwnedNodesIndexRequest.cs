using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Home.Nodes;

[PublicAPI]
public sealed class OwnedNodesIndexRequest : IHttpRequest<IReplica<IReadOnlyCollection<string>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/account/home/nodes")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public OwnedNodesIndexRequest(string? accessToken)
    {
        AccessToken = accessToken;
    }

    public string? AccessToken { get; }

    public async Task<IReplica<IReadOnlyCollection<string>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetStringRequired());
        return new Replica<IReadOnlyCollection<string>>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
