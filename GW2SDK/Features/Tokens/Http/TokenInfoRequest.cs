using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Tokens.Json;
using GW2SDK.Tokens.Models;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Tokens.Http;

[PublicAPI]
public sealed class TokenInfoRequest : IHttpRequest<IReplica<TokenInfo>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/tokeninfo")
    {
        AcceptEncoding = "gzip"
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
        QueryBuilder search = new();
        var request = Template with
        {
            Arguments = search,
            BearerToken = AccessToken
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = TokenInfoReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<TokenInfo>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
