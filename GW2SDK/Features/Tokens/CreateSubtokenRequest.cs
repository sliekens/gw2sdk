using GuildWars2.Http;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Tokens;

[PublicAPI]
public sealed class CreateSubtokenRequest : IHttpRequest<Replica<CreatedSubtoken>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/createsubtoken")
    {
        AcceptEncoding = "gzip"
    };

    public CreateSubtokenRequest(string accessToken)
    {
        AccessToken = accessToken;
    }

    public string AccessToken { get; }

    public DateTimeOffset? AbsoluteExpirationDate { get; init; }

    public IReadOnlyCollection<Permission>? Permissions { get; init; }

    public IReadOnlyCollection<string>? Urls { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<CreatedSubtoken>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder args = new();
        if (Permissions is { Count: not 0 })
        {
            args.Add("permissions", string.Join(",", Permissions).ToLowerInvariant());
        }

        if (AbsoluteExpirationDate.HasValue)
        {
            args.Add("expire", AbsoluteExpirationDate.Value.ToUniversalTime().ToString("s"));
        }

        if (Urls is { Count: not 0 })
        {
            args.Add("urls", string.Join(",", Urls));
        }

        args.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = args,
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<CreatedSubtoken>
        {
            Value = json.RootElement.GetCreatedSubtoken(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
