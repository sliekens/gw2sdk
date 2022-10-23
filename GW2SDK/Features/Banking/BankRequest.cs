using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Banking;

[PublicAPI]
public sealed class BankRequest : IHttpRequest<IReplica<Bank>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/bank")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Bank>> SendAsync(
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

        var value = json.RootElement.GetBank(MissingMemberBehavior);
        return new Replica<Bank>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
