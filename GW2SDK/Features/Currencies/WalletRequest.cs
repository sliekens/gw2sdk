using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Currencies;

[PublicAPI]
public sealed class WalletRequest : IHttpRequest<IReplica<IReadOnlyCollection<CurrencyAmount>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/account/wallet")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<IReadOnlyCollection<CurrencyAmount>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(item => item.GetCurrencyAmount(MissingMemberBehavior));
        return new Replica<IReadOnlyCollection<CurrencyAmount>>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
