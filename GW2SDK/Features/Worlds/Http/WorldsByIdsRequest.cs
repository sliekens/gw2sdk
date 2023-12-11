using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Worlds.Http;

internal sealed class WorldsByIdsRequest : IHttpRequest<HashSet<World>>
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

    public async Task<(HashSet<World> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => entry.GetWorld(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
