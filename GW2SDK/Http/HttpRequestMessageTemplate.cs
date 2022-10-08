using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace GW2SDK.Http;

[PublicAPI]
public sealed record HttpRequestMessageTemplate(HttpMethod Method, string Path)
{
    private QueryBuilder queryBuilder = QueryBuilder.Empty;

    public string? AcceptEncoding { get; init; }

    public string? AcceptLanguage { get; init; }

    public string? BearerToken { get; init; }

    public QueryBuilder Arguments
    {
        get => queryBuilder;
        init
        {
            queryBuilder = value;
            queryBuilder.Freeze();
        }
    }

    public SchemaVersion? SchemaVersion { get; init; } = SchemaVersion.Default;

    public HttpRequestMessage Compile()
    {
        var arguments = Arguments.Clone();
        if (SchemaVersion is not null)
        {
            arguments.Add("v", SchemaVersion.Version);
        }

        var location = Path;
        if (arguments.Count != 0)
        {
            location += arguments;
        }

        HttpRequestMessage message = new(Method, new Uri(location, UriKind.Relative));
        if (!string.IsNullOrWhiteSpace(BearerToken))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
        }

        if (!string.IsNullOrWhiteSpace(AcceptEncoding))
        {
            message.Headers.AcceptEncoding.ParseAdd(AcceptEncoding);
        }

        if (!string.IsNullOrWhiteSpace(AcceptLanguage))
        {
            message.Headers.AcceptLanguage.ParseAdd(AcceptLanguage);
        }

        return message;
    }
}
