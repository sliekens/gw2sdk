using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Worlds.Http;

internal sealed class WorldsByIdsRequest : IHttpRequest<Replica<HashSet<World>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/worlds")
    {
        AcceptEncoding = "gzip"
    };

    public WorldsByIdsRequest(IReadOnlyCollection<int> worldIds)
    {
        Check.Collection(worldIds);
        WorldIds = worldIds;
    }

    public IReadOnlyCollection<int> WorldIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<World>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", WorldIds },
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
        return new Replica<HashSet<World>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetWorld(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
