using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Maps;

[PublicAPI]
public sealed class MapSummariesByIdsRequest : IHttpRequest<Replica<HashSet<MapSummary>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/maps")
    {
        AcceptEncoding = "gzip"
    };

    public MapSummariesByIdsRequest(IReadOnlyCollection<int> mapIds)
    {
        Check.Collection(mapIds);
        MapIds = mapIds;
    }

    public IReadOnlyCollection<int> MapIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<MapSummary>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MapIds },
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
        return new Replica<HashSet<MapSummary>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetMapSummary(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
