﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.MapChests;

[PublicAPI]
public sealed class MapChestsIndexRequest : IHttpRequest<IReplicaSet<string>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/mapchests")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public async Task<IReplicaSet<string>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetStringRequired());
        return new ReplicaSet<string>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}