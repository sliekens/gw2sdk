using System.Net.Http.Headers;

namespace GuildWars2.Http;

internal static class Request
{
    public static HttpRequestMessage HttpGet(string path, QueryBuilder query, string? accessToken)
    {
        var message = new HttpRequestMessage(Get, path + query);
        message.Headers.AcceptEncoding.ParseAdd("gzip");
        if (!string.IsNullOrEmpty(accessToken))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return message;
    }
}
