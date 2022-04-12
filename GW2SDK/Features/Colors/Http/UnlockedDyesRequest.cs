using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http;

[PublicAPI]
public sealed class UnlockedDyesRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/dyes")
    {
        AcceptEncoding = "gzip"
    };

    public UnlockedDyesRequest(string? accessToken)
    {
        AccessToken = accessToken;
    }

    public string? AccessToken { get; }

    public static implicit operator HttpRequestMessage(UnlockedDyesRequest r)
    {
        var request = Template with
        {
            BearerToken = r.AccessToken
        };
        return request.Compile();
    }
}