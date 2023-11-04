using GuildWars2.Http;

namespace GuildWars2.Tokens.Http;

internal sealed class CreateSubtokenRequest : IHttpRequest2<CreatedSubtoken>
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

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(CreatedSubtoken Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetCreatedSubtoken(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
