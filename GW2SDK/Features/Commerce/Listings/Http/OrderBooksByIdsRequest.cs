using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Commerce.Listings.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Listings.Http;

[PublicAPI]
public sealed class OrderBooksByIdsRequest : IHttpRequest<IReplicaSet<OrderBook>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/listings")
    {
        AcceptEncoding = "gzip"
    };

    public OrderBooksByIdsRequest(IReadOnlyCollection<int> itemIds)
    {
        Check.Collection(itemIds, nameof(itemIds));
        ItemIds = itemIds;
    }

    public IReadOnlyCollection<int> ItemIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<OrderBook>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("ids", ItemIds);
        var request = Template with { Arguments = search };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value =
            json.RootElement.GetSet(entry => OrderBookReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<OrderBook>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
