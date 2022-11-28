using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Commerce.Listings;

[PublicAPI]
public sealed class OrderBookByIdRequest : IHttpRequest<IReplica<OrderBook>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/commerce/listings")
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
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", ItemId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
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
