using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Commerce.Exchange.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Exchange.Http;

[PublicAPI]
public sealed class ExchangeGoldForGemsRequest : IHttpRequest<IReplica<GoldForGemsExchange>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/commerce/exchange/coins") { AcceptEncoding = "gzip" };

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
        QueryBuilder search = new();
        search.Add("quantity", CoinsCount);
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

        var value = GoldForGemsReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<GoldForGemsExchange>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
