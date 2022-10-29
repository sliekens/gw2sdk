using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Tokens;

[PublicAPI]
public sealed class TokenInfoRequest : IHttpRequest<IReplica<TokenInfo>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/tokeninfo")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public TokenInfoRequest(string accessToken)
    {
        AccessToken = accessToken;
    }

    public string AccessToken { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<TokenInfo>> SendAsync(
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

        var value = json.RootElement.GetTokenInfo(MissingMemberBehavior);
        return new Replica<TokenInfo>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
