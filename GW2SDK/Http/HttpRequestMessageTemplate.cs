using System.Net.Http.Headers;

namespace GuildWars2.Http;

[PublicAPI]
public sealed record HttpRequestMessageTemplate(HttpMethod Method, string Path)
{
    private readonly QueryBuilder queryBuilder = QueryBuilder.Empty;

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

    public HttpRequestMessage Compile()
    {
        Uri location = new(Path + Arguments, UriKind.Relative);
        HttpRequestMessage message = new(Method, location);
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

    public static implicit operator
        HttpRequestMessage(HttpRequestMessageTemplate requestTemplate) =>
        requestTemplate.Compile();
}
