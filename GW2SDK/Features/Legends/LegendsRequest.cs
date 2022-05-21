﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Legends;

[PublicAPI]
public sealed class LegendsRequest : IHttpRequest<IReplicaSet<Legend>>
{
    private static readonly HttpRequestMessageTemplate Template = new(HttpMethod.Get, "/v2/legends")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "ids", "all" } }
    };

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Legend>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response =
            await httpClient.SendAsync(Template, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(
            entry => LegendReader.GetLegend(entry, MissingMemberBehavior)
        );
        return new ReplicaSet<Legend>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
