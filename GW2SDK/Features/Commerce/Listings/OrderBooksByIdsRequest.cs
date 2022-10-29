using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Listings;

[PublicAPI]
public sealed class OrderBooksByIdsRequest : IHttpRequest<IReplicaSet<OrderBook>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/commerce/listings")
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
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ItemIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetOrderBook(MissingMemberBehavior));
        return new ReplicaSet<OrderBook>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
