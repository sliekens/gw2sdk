using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class UnlockedRecipesRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/recipes")
    {
        AcceptEncoding = "gzip"
    };

    public UnlockedRecipesRequest(string? accessToken)
    {
        AccessToken = accessToken;
    }

    public string? AccessToken { get; }

    public static implicit operator HttpRequestMessage(UnlockedRecipesRequest r)
    {
        var request = Template with
        {
            BearerToken = r.AccessToken
        };
        return request.Compile();
    }
}
