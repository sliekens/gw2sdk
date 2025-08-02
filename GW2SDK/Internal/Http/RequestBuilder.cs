using System.Net.Http.Headers;

namespace GuildWars2.Http;

internal sealed class RequestBuilder(HttpMethod method, string path, string? accessToken)
{

#pragma warning disable RCS1250 // Use implicit/explicit object creation
    /// <summary>The query builder for this request. The schema version is always added as the first query parameter.</summary>
    /// <remarks>
    /// The schema version contains a colon, which can cause <see cref="Uri"/> to treat it as a scheme separator.
    /// In .NET Framework, this can result in a <see cref="UriFormatException"/> with the message "Invalid URI: The Uri scheme is too long."
    /// To avoid this bug, ensure the version is added as the first query parameter, before any others.
    /// The bug occurs when a colon is found in a relative URI after 1024 other characters.
    /// </remarks>
    public QueryBuilder Query { get; } = new() { { "v", SchemaVersion.Recommended } };
#pragma warning restore RCS1250 // Use implicit/explicit object creation

    public HttpRequestMessage Build()
    {
        Uri location = new(path + Query, UriKind.Relative);
        HttpRequestMessage message = new(method, location);
        message.Headers.AcceptEncoding.ParseAdd("gzip");
        if (!string.IsNullOrEmpty(accessToken))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return message;
    }

    public static RequestBuilder HttpGet(string path, string? accessToken = null)
    {
        return new(Get, path, accessToken);
    }

    public override string ToString()
    {
        return path + Query;
    }
}
