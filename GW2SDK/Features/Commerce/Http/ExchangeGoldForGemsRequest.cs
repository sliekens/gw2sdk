using GuildWars2.Commerce.Exchange;
using GuildWars2.Http;

namespace GuildWars2.Commerce.Http;

internal sealed class ExchangeGoldForGemsRequest : IHttpRequest<GoldForGemsExchange>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/commerce/exchange/coins") { AcceptEncoding = "gzip" };

    public ExchangeGoldForGemsRequest(int coinsCount)
    {
        CoinsCount = coinsCount;
    }

    public int CoinsCount { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(GoldForGemsExchange Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "quantity", CoinsCount },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetGoldForGemsExchange(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
