using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Http;

[PublicAPI]
public sealed class AccountRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account")
    {
        AcceptEncoding = "gzip"
    };

    public AccountRequest(string? accessToken)
    {
        AccessToken = accessToken;
    }

    public string? AccessToken { get; }

    public static implicit operator HttpRequestMessage(AccountRequest r)
    {
        var request = Template with
        {
            BearerToken = r.AccessToken
        };
        return request.Compile();
    }
}
