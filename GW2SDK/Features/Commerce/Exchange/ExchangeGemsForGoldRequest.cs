using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Commerce.Exchange;

[PublicAPI]
public sealed class ExchangeGemsForGoldRequest : IHttpRequest<IReplica<GemsForGoldExchange>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/commerce/exchange/gems") { AcceptEncoding = "gzip" };

    public ExchangeGemsForGoldRequest(int gemsCount)
    {
        GemsCount = gemsCount;
    }

    public int GemsCount { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<GemsForGoldExchange>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "quantity", GemsCount },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetGemsForGoldExchange(MissingMemberBehavior);
        return new Replica<GemsForGoldExchange>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
