using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Outfits.Http;

internal sealed class OutfitsByIdsRequest : IHttpRequest<HashSet<Outfit>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/outfits") { AcceptEncoding = "gzip" };

    public OutfitsByIdsRequest(IReadOnlyCollection<int> outfitIds)
    {
        Check.Collection(outfitIds);
        OutfitIds = outfitIds;
    }

    public IReadOnlyCollection<int> OutfitIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Outfit> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", OutfitIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetOutfit(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
