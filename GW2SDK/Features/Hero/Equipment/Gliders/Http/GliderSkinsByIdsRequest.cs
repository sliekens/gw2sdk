using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Gliders.Http;

internal sealed class GliderSkinsByIdsRequest : IHttpRequest<HashSet<GliderSkin>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/gliders") { AcceptEncoding = "gzip" };

    public GliderSkinsByIdsRequest(IReadOnlyCollection<int> gliderSkinIds)
    {
        Check.Collection(gliderSkinIds);
        GliderSkinIds = gliderSkinIds;
    }

    public IReadOnlyCollection<int> GliderSkinIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<GliderSkin> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", GliderSkinIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetGliderSkin(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
