using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.JadeBots.Http;

internal sealed class JadeBotSkinByIdRequest(int jadeBotSkinId) : IHttpRequest<JadeBotSkin>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/jadebots") { AcceptEncoding = "gzip" };

    public int JadeBotSkinId { get; } = jadeBotSkinId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(JadeBotSkin Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", JadeBotSkinId },
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
        var value = json.RootElement.GetJadeBotSkin(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
