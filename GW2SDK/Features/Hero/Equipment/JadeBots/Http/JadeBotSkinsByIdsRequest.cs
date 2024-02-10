using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.JadeBots.Http;

internal sealed class JadeBotSkinsByIdsRequest : IHttpRequest<HashSet<JadeBotSkin>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/jadebots") { AcceptEncoding = "gzip" };

    public JadeBotSkinsByIdsRequest(IReadOnlyCollection<int> jadeBotSkinIds)
    {
        Check.Collection(jadeBotSkinIds);
        JadeBotSkinIds = jadeBotSkinIds;
    }

    public IReadOnlyCollection<int> JadeBotSkinIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<JadeBotSkin> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", JadeBotSkinIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetJadeBotSkin(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
