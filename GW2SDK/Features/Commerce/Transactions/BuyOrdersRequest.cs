﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Transactions;

[PublicAPI]
public sealed class BuyOrdersRequest : IHttpRequest<IReplicaPage<Order>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/commerce/transactions/current/buys") { AcceptEncoding = "gzip" };

    public BuyOrdersRequest(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaPage<Order>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("page", PageIndex);
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        var request = Template with
        {
            Arguments = search,
            BearerToken = AccessToken
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetOrder(MissingMemberBehavior));
        return new ReplicaPage<Order>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetPageContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}