using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Commerce.Exchange;

[PublicAPI]
public sealed class ExchangeGoldForGemsRequest : IHttpRequest<IReplica<GoldForGemsExchange>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/commerce/exchange/coins") { AcceptEncoding = "gzip" };

    public ExchangeGoldForGemsRequest(int coinsCount)
    {
        CoinsCount = coinsCount;
    }

    public int CoinsCount { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<GoldForGemsExchange>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "quantity", CoinsCount },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new Replica<GoldForGemsExchange>
        {
            Value = json.RootElement.GetGoldForGemsExchange(MissingMemberBehavior),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
