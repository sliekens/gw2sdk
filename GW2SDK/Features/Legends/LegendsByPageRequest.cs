﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Legends;

[PublicAPI]
public sealed class LegendsByPageRequest : IHttpRequest<IReplicaPage<Legend>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/legends") { AcceptEncoding = "gzip" };

    public LegendsByPageRequest(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaPage<Legend>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "page", PageIndex } };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        search.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with { Arguments = search },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(
            entry => LegendReader.GetLegend(entry, MissingMemberBehavior)
        );
        return new ReplicaPage<Legend>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetPageContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
