using GuildWars2.Http;
using GuildWars2.Pvp.Games;

namespace GuildWars2.Pvp.Http;

internal sealed class GameByIdRequest : IHttpRequest<Replica<Game>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/games") { AcceptEncoding = "gzip" };

    public GameByIdRequest(string gameId)
    {
        GameId = gameId;
    }

    public string GameId { get; }

    public required string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Game>> SendAsync(
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
        return new Replica<Game>
        {
            Value = json.RootElement.GetGame(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
