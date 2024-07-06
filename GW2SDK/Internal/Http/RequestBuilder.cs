using System.Net.Http.Headers;

namespace GuildWars2.Http;

internal sealed class RequestBuilder(HttpMethod method, string path, string? accessToken)
{
    // Note: the schema version contains a colon, which can cause System.Uri to treat it as a scheme separator
    // In .NET Framework, this can cause System.UriFormatException, Invalid URI: The Uri scheme is too long.
    // So make sure the version is added as the first query parameter, before any other parameters, to avoid the bug
    // (the bug occurs when a colon is found in a relative URI after 1024 other characters)
    public QueryBuilder Query { get; } = new() { { "v", SchemaVersion.Recommended } };

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

    public static RequestBuilder HttpGet(string path, string? accessToken = null) =>
        new(Get, path, accessToken);

    public override string ToString() => path + Query;
}
