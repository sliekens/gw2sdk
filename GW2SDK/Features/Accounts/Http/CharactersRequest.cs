using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Http;

[PublicAPI]
public sealed class CharactersRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/characters")
    {
        AcceptEncoding = "gzip"
    };

    public CharactersRequest(string? accessToken)
    {
        AccessToken = accessToken;
    }

    public string? AccessToken { get; }

    public static implicit operator HttpRequestMessage(CharactersRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", "all");
        var request = Template with
        {
            BearerToken = r.AccessToken,
            Arguments = search
        };
        return request.Compile();
    }
}