using GuildWars2.Exploration.Continents;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class ContinentsByIdsRequest : IHttpRequest<HashSet<Continent>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/continents")
    {
        AcceptEncoding = "gzip"
    };

    public ContinentsByIdsRequest(IReadOnlyCollection<int> continentIds)
    {
        Check.Collection(continentIds);
        ContinentIds = continentIds;
    }

    public IReadOnlyCollection<int> ContinentIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Continent> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ContinentIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetContinent(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
