using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Currencies.Http;

internal sealed class CurrenciesByIdsRequest : IHttpRequest<Replica<HashSet<Currency>>>
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

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Currency>>> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetCurrency(MissingMemberBehavior));
        return new Replica<HashSet<Currency>>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
