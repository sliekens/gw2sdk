using GuildWars2.Http;
using GuildWars2.Pvp.Games;

namespace GuildWars2.Pvp.Http;

internal sealed class GameByIdRequest : IHttpRequest<Game>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/games") { AcceptEncoding = "gzip" };

    public GameByIdRequest(string gameId)
    {
        GameId = gameId;
    }

    public string GameId { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Game Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", GameId },
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
        var value = json.RootElement.GetGame(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
