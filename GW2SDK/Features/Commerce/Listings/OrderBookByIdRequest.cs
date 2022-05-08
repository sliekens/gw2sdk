using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Listings;

[PublicAPI]
public sealed class OrderBookByIdRequest : IHttpRequest<IReplica<OrderBook>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/listings")
    {
        AcceptEncoding = "gzip"
    };

    public OrderBookByIdRequest(int itemId)
    {
        ItemId = itemId;
    }

    public int ItemId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<OrderBook>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "id", ItemId } };
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

        var value = json.RootElement.GetOrderBook(MissingMemberBehavior);
        return new Replica<OrderBook>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
