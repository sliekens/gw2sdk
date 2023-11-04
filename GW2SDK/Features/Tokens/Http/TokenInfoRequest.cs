using GuildWars2.Http;

namespace GuildWars2.Tokens.Http;

internal sealed class TokenInfoRequest : IHttpRequest2<TokenInfo>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/tokeninfo")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public TokenInfoRequest(string accessToken)
    {
        AccessToken = accessToken;
    }

    public string AccessToken { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(TokenInfo Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { BearerToken = AccessToken }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetTokenInfo(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
