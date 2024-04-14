using GuildWars2.Http;

namespace GuildWars2.Authorization.Http;

internal sealed class CreateSubtokenRequest(string accessToken) : IHttpRequest<CreatedSubtoken>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/createsubtoken")
    {
        AcceptEncoding = "gzip"
    };

    public string AccessToken { get; } = accessToken;

    public DateTimeOffset? AbsoluteExpirationDate { get; init; }

    public IReadOnlyCollection<Permission>? Permissions { get; init; }

    public IReadOnlyCollection<string>? AllowedUrls { get; init; }

    
    public async Task<(CreatedSubtoken Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder args = [];
        if (Permissions?.Count is not null and not 0)
        {
            args.Add("permissions", string.Join(",", Permissions).ToLowerInvariant());
        }

        if (AbsoluteExpirationDate.HasValue)
        {
            args.Add("expire", AbsoluteExpirationDate.Value.ToUniversalTime().ToString("s"));
        }

        if (AllowedUrls?.Count is not null and not 0)
        {
            args.Add("urls", string.Join(",", AllowedUrls));
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
        var value = json.RootElement.GetCreatedSubtoken();
        return (value, new MessageContext(response));
    }
}
