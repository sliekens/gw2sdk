using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts;

[PublicAPI]
public sealed class AccountRequest : IHttpRequest<IReplica<AccountSummary>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/account")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<AccountSummary>> SendAsync(
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

        return new Replica<AccountSummary>(
            response.Headers.Date.GetValueOrDefault(),
            json.RootElement.GetAccountSummary(MissingMemberBehavior),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
