using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Currencies.Http;

internal sealed class WalletRequest : IHttpRequest<HashSet<CurrencyAmount>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/account/wallet")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<CurrencyAmount> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { BearerToken = AccessToken }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(item => item.GetCurrencyAmount(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
