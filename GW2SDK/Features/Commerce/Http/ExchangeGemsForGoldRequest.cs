using GuildWars2.Commerce.Exchange;
using GuildWars2.Http;

namespace GuildWars2.Commerce.Http;

internal sealed class ExchangeGemsForGoldRequest : IHttpRequest2<GemsForGoldExchange>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/commerce/exchange/gems") { AcceptEncoding = "gzip" };

    public ExchangeGemsForGoldRequest(int gemsCount)
    {
        GemsCount = gemsCount;
    }

    public int GemsCount { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(GemsForGoldExchange Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "quantity", GemsCount },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetGemsForGoldExchange(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
