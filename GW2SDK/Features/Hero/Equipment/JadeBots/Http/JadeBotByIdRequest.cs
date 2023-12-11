using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.JadeBots.Http;

internal sealed class JadeBotByIdRequest(int jadeBotId) : IHttpRequest<JadeBot>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/jadebots") { AcceptEncoding = "gzip" };

    public int JadeBotId { get; } = jadeBotId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(JadeBot Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", JadeBotId },
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
        var value = json.RootElement.GetJadeBot(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
