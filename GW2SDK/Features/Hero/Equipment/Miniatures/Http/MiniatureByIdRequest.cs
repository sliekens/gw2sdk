using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Miniatures.Http;

internal sealed class MiniatureByIdRequest(int miniatureId) : IHttpRequest<Miniature>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/minis") { AcceptEncoding = "gzip" };

    public int MiniatureId { get; } = miniatureId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Miniature Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MiniatureId },
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
        var value = json.RootElement.GetMiniature(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
