using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Pvp.Games;

namespace GuildWars2.Pvp.Http;

internal sealed class GamesByIdsRequest : IHttpRequest2<HashSet<Game>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/pvp/games")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    public GamesByIdsRequest(IReadOnlyCollection<string> gameIds)
    {
        Check.Collection(gameIds);
        GameIds = gameIds;
    }

    public IReadOnlyCollection<string> GameIds { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Game> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", GameIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetGame(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
