using System.Net.Http.Headers;

namespace GuildWars2.Http;

internal sealed class RequestBuilder(HttpMethod method, string path, string? accessToken)
{
    public QueryBuilder Query { get; } = [];

    public HttpRequestMessage Build()
    {
        var location = new Uri(path + Query, UriKind.Relative);
        var message = new HttpRequestMessage(method, location);
        message.Headers.AcceptEncoding.ParseAdd("gzip");
        if (!string.IsNullOrEmpty(accessToken))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return message;
    }

    public static RequestBuilder HttpGet(string path, string? accessToken = null) => new(Get, path, accessToken);

    public override string ToString() => path + Query;
}
