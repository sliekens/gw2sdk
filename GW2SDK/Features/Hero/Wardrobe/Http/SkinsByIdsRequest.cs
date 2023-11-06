using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Wardrobe.Http;

internal sealed class SkinsByIdsRequest : IHttpRequest<HashSet<Skin>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/skins")
    {
        AcceptEncoding = "gzip"
    };

    public SkinsByIdsRequest(IReadOnlyCollection<int> skinIds)
    {
        Check.Collection(skinIds);
        SkinIds = skinIds;
    }

    public IReadOnlyCollection<int> SkinIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Skin> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", SkinIds },
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
        var value = json.RootElement.GetSet(entry => SkinJson.GetSkin(entry, MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
