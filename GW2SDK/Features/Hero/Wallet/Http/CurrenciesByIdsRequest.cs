using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Wallet.Http;

internal sealed class CurrenciesByIdsRequest : IHttpRequest<HashSet<Currency>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/currencies")
    {
        AcceptEncoding = "gzip"
    };

    public CurrenciesByIdsRequest(IReadOnlyCollection<int> currencyIds)
    {
        Check.Collection(currencyIds);
        CurrencyIds = currencyIds;
    }

    public IReadOnlyCollection<int> CurrencyIds { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<Currency> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", CurrencyIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(static entry => entry.GetCurrency());
        return (value, new MessageContext(response));
    }
}
