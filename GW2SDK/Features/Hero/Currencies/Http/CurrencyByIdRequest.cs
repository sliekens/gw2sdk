using GuildWars2.Http;

namespace GuildWars2.Hero.Currencies.Http;

internal sealed class CurrencyByIdRequest : IHttpRequest<Currency>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/currencies")
    {
        AcceptEncoding = "gzip"
    };

    public CurrencyByIdRequest(int currencyId)
    {
        CurrencyId = currencyId;
    }

    public int CurrencyId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Currency Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", CurrencyId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetCurrency(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
