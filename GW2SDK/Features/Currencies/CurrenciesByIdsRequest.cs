using GuildWars2.Http;
using GuildWars2.Json;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Currencies;

[PublicAPI]
public sealed class CurrenciesByIdsRequest : IHttpRequest<Replica<HashSet<Currency>>>
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

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<Currency>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetCurrency(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
