using GuildWars2.Http;

namespace GuildWars2.Worlds.Http;

internal sealed class WorldByIdRequest(int worldId) : IHttpRequest<World>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/worlds")
    {
        AcceptEncoding = "gzip"
    };

    public int WorldId { get; } = worldId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(World Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", WorldId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetWorld(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
