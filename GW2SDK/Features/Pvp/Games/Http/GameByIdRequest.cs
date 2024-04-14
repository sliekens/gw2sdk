using GuildWars2.Http;

namespace GuildWars2.Pvp.Games.Http;

internal sealed class GameByIdRequest(string gameId) : IHttpRequest<Game>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/games") { AcceptEncoding = "gzip" };

    public string GameId { get; } = gameId;

    public required string? AccessToken { get; init; }

    
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetGame();
        return (value, new MessageContext(response));
    }
}
