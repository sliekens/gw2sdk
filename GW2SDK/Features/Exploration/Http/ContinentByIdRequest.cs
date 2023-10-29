using GuildWars2.Exploration.Continents;
using GuildWars2.Http;

namespace GuildWars2.Exploration.Http;

internal sealed class ContinentByIdRequest : IHttpRequest<Replica<Continent>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/continents")
    {
        AcceptEncoding = "gzip"
    };

    public ContinentByIdRequest(int continentId)
    {
        ContinentId = continentId;
    }

    public int ContinentId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Continent>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", ContinentId },
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
        return new Replica<Continent>
        {
            Value = json.RootElement.GetContinent(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
