using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Tokens.Http;

[PublicAPI]
public sealed class TokenInfoRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/tokeninfo")
    {
        AcceptEncoding = "gzip"
    };

    public TokenInfoRequest(string? accessToken)
    {
        AccessToken = accessToken;
    }

    public string? AccessToken { get; }

    public static implicit operator HttpRequestMessage(TokenInfoRequest r)
    {
        var request = Template with
        {
            BearerToken = r.AccessToken
        };
        return request.Compile();
    }
}